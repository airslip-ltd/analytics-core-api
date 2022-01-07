using Airslip.Common.Services.Handoff.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Airslip.Analytics.Processor.Functions
{
    public static class YapilyAccounts
    {
        [Function(nameof(YapilyAccounts))]
        public static async Task Run([EventHubTrigger("yapily-accounts", 
            Connection = "YapilyEventHubConnectionString",
            ConsumerGroup = "%ConsumerGroup%",
            IsBatched = false)] string myEventHubMessage, FunctionContext context)
        {
            IMessageHandoffService messageService = context
                .InstanceServices
                .GetService<IMessageHandoffService>() ?? throw new NotImplementedException();

            await messageService.ProcessMessage("yapily-accounts", myEventHubMessage);
        }
    }
}