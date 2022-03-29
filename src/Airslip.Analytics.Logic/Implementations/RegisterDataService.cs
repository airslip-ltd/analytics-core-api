using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;
using Airslip.Common.Utilities;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using Serilog;

namespace Airslip.Analytics.Logic.Implementations
{
    public class RegisterDataService<TEntity, TModel, TRawModel> : IRegisterDataService<TEntity, TModel, TRawModel>
        where TModel : class, IModel, IFromDataSource
        where TEntity : class, IEntity, IFromDataSource
    {
        private readonly ILogger _logger;
        private readonly IRepository<TEntity, TModel> _repository;
        private readonly IModelMapper<TModel> _modelMapper;
        private readonly IEnumerable<IAnalysisMessagingService<TModel>> _postProcessors;

        public RegisterDataService(
            ILogger logger,
            IRepository<TEntity, TModel> repository,
            IModelMapper<TModel> modelMapper,
            IEnumerable<IAnalysisMessagingService<TModel>> postProcessors)
        {
            _logger = logger;
            _repository = repository;
            _modelMapper = modelMapper;
            _postProcessors = postProcessors;
        }

        private async Task RegisterData(TRawModel rawModel, DataSources dataSource)
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
                    
                    if (model.EntityStatus == EntityStatus.Deleted)
                        await _repository.Delete(id, userId);
                    
                    switch (result)
                    {
                        case SuccessfulActionResultModel<TModel> {CurrentVersion: { }} success:
                        {
                            foreach (IAnalysisMessagingService<TModel> analyticsProcess in _postProcessors)
                            {
                                await analyticsProcess.RequestAnalysis(success.CurrentVersion);
                            }
                            break;
                        }
                        case FailedActionResultModel<TModel> failed:
                            _logger.Error("Repository action failed with code {ErrorCode} for model {@ValidationResult}", 
                                failed.ErrorCode, failed.ValidationResult);
                            break;
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

        public Task Execute(string message, DataSources dataSource)
        {
            // Turn to object
            TRawModel bankTransaction = Json.Deserialize<TRawModel>(message);

            return RegisterData( bankTransaction, dataSource);
        }
    }
}