using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Enums;
using Airslip.Common.Utilities;
using Airslip.Common.Utilities.Extensions;
using Microsoft.Data.SqlClient;
using Polly;
using Polly.Retry;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Airslip.Analytics.Core.Implementations
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
                .Handle<SqlException>()
                .RetryAsync(maxRetryAttempts);

            try
            {
                TModel model = _modelMapper
                    .Create(rawModel);
                
                model.DataSource = dataSource;
                model.TimeStamp = DateTime.UtcNow.ToUnixTimeMilliseconds();
                
                string id = model.Id ?? string.Empty;
                await retryPolicy.ExecuteAsync(async () =>
                {
                    string? userId = model is IModelWithOwnership ownedModel ? ownedModel.UserId : null;
                    RepositoryActionResultModel<TModel> result = await _repository.Upsert(id, model, userId);

                    if (result is SuccessfulActionResultModel<TModel> {CurrentVersion: { }} success)
                    {
                        foreach (IAnalyticsProcess<TModel> analyticsProcess in _postProcessors)
                        {
                            await analyticsProcess.Execute(success.CurrentVersion);
                        }
                    }
                });
            }
            catch (SqlException se)
            {
                _logger.Fatal(se, "SqlException exception for the data source {DataSource} with data packet {Model}", dataSource, rawModel);
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