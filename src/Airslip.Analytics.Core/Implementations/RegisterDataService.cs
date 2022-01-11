using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Utilities;
using Airslip.Common.Utilities.Extensions;
using Microsoft.Data.SqlClient;
using Polly;
using Polly.Retry;
using Serilog;
using System;
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

        public RegisterDataService(
            ILogger logger,
            IRepository<TEntity, TModel> repository,
            IModelMapper<TModel> modelMapper)
        {
            _logger = logger;
            _repository = repository;
            _modelMapper = modelMapper;
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
                    if (model is IModelWithOwnership ownedModel)
                        await _repository.Upsert(id, model, ownedModel.UserId);
                    else
                        await _repository.Upsert(id, model);
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