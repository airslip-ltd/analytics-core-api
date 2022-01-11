using Airslip.Analytics.Core.Constants;
using Airslip.Common.Services.Handoff.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Airslip.Analytics.Processor.Functions
{
    public static class YapilyBalances
    {
        [Function(nameof(YapilyBalances))]
        public static async Task Run([EventHubTrigger(Constants.EVENT_QUEUE_YAPILY_BALANCES, 
            Connection = "YapilyEventHubConnectionString",
            ConsumerGroup = "%ConsumerGroup%",
            IsBatched = false)] string myEventHubMessage, FunctionContext context)
        {
            IMessageHandoffService messageService = context
                .InstanceServices
                .GetService<IMessageHandoffService>() ?? throw new NotImplementedException();

            await messageService.ProcessMessage(Constants.EVENT_QUEUE_YAPILY_BALANCES, myEventHubMessage);
        }
    }
}