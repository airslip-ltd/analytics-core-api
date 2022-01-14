using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;
using Airslip.Common.Utilities;
using Airslip.Common.Utilities.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airslip.Analytics.Services.SqlServer.Implementations
{
    public class RegisterDataService<TEntity, TModel, TRawModel> : IRegisterDataService<TEntity, TModel, TRawModel>
        where TModel : class, IModel, IFromDataSource
        where TEntity : class, IEntity, IFromDataSource
    {
        private readonly ILogger _logger;
        private readonly IRepository<TEntity, TModel> _repository;
        private readonly IModelMapper<TModel> _modelMapper;
        private readonly IEnumerable<IAnalyticsProcess<TModel>> _postProcessors;

        public RegisterDataService(
            ILogger logger,
            IRepository<TEntity, TModel> repository,
            IModelMapper<TModel> modelMapper,
            IEnumerable<IAnalyticsProcess<TModel>> postProcessors)
        {
            _logger = logger;
            _repository = repository;
            _modelMapper = modelMapper;
            _postProcessors = postProcessors;
        }

        public async Task RegisterData(TRawModel rawModel, DataSources dataSource)
        {
            int maxRetryAttempts = 3;
            
            AsyncRetryPolicy? retryPolicy = Policy
                .Handle<DbUpdateException>()
                .WaitAndRetryAsync(maxRetryAttempts,  retryAttempt => 
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            try
            {
                TModel model = _modelMapper
                    .Create(rawModel);
                
                model.DataSource = dataSource;
                
                string id = model.Id ?? string.Empty;
                int count = 0;
                await retryPolicy.ExecuteAsync(async () =>
                {
                    count++;
                    if (count > 1) _logger.Information("Retrying attempt {Attempt}", count);
                    
                    string? userId = model is IModelWithOwnership ownedModel ? ownedModel.UserId : null;

                    RepositoryActionResultModel<TModel> result = await _repository.Upsert(id, model, userId);

                    if (result is SuccessfulActionResultModel<TModel> {CurrentVersion: { }} success)
                    {
                        foreach (IAnalyticsProcess<TModel> analyticsProcess in _postProcessors)
                        {
                            int affectedRows = await 
                                analyticsProcess.Execute(success.CurrentVersion);
                            _logger.Information("Executed analytics task, {AffectedRows}", 
                                affectedRows);
                        }
                    }

                    if (result is FailedActionResultModel<TModel> failed)
                    {
                        _logger.Error("Repository action failed with code {ErrorCode} for model {Model}", 
                            failed.ErrorCode, model);
                    }
                });
            }
            catch (DbUpdateException se)
            {
                _logger.Fatal(se, "DbUpdateException exception for the data source {DataSource} with data packet {Model}", dataSource, rawModel);
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Unhandled exception for the data source {DataSource} with data packet {Model}", dataSource, rawModel);
            }
        }

        public Task RegisterData(string message, DataSources dataSource)
        {
            // Turn to object
            TRawModel bankTransaction = Json.Deserialize<TRawModel>(message);

            return RegisterData( bankTransaction, dataSource);
        }

        public Task Execute(string message)
        {
            return RegisterData(message, DataSources.Yapily);
        }
    }
}