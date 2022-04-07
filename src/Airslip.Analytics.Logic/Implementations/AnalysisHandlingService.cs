using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Metrics.Enums;
using Airslip.Common.Metrics.Interfaces;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Services.Handoff.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Utilities;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using Serilog;

namespace Airslip.Analytics.Logic.Implementations;

public class AnalysisHandlingService<TModel> : IAnalysisHandlingService<TModel> 
    where TModel : class, ITraceable
{
    private readonly IEnumerable<IAnalyticsProcess<TModel>> _postProcessors;
    private readonly IMetricService _metricService;
    private readonly ILogger _logger;

    public AnalysisHandlingService(IEnumerable<IAnalyticsProcess<TModel>> postProcessors,
        IMetricService metricService,
        ILogger logger)
    {
        _postProcessors = postProcessors;
        _metricService = metricService;
        _logger = logger;
    }
    
    public async Task Execute(TModel model)
    {
        int maxRetryAttempts = 3;
    
        AsyncRetryPolicy? retryPolicy = Policy
            .Handle<DbUpdateException>()
            .WaitAndRetryAsync(maxRetryAttempts,  retryAttempt => 
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        
        foreach (IAnalyticsProcess<TModel> analyticsProcess in _postProcessors)
        {
            _metricService.StartActivity(nameof(AnalysisHandlingService<TModel>));
            _metricService.LogMetric(analyticsProcess.GetType().FullName!, MetricType.Start);
            try
            {
                await retryPolicy.ExecuteAsync(async () => {
                    int affectedRows = await analyticsProcess.Execute(model);
                    _logger.Information("Executed analytics task, {AffectedRows}",
                        affectedRows);
                });
            }
            catch (DbUpdateException se)
            {
                _logger.Error(se, "Error executing analytics process {ClassName}, trace info {TraceInfo}", 
                    analyticsProcess.GetType().FullName, model.TraceInfo);
            }
            catch (Exception eee)
            {
                _logger.Error(eee, "Error executing analytics process {ClassName}", 
                    analyticsProcess.GetType().FullName);
            }
            _metricService.LogMetric(analyticsProcess.GetType().FullName!, MetricType.Complete);
            _metricService.StopActivity();

        }
    }

    public Task Execute(string message, DataSources dataSource)
    {
        return Execute(Json.Deserialize<TModel>(message));
    }
}