using Airslip.Analytics.Core.Data;
using Airslip.Common.Services.Handoff.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Airslip.Analytics.Processor.Functions.EventHub.CustomerPortal;

public static class Business
{
    [Function(nameof(Business))]
    public static async Task Run([EventHubTrigger(Constants.EVENT_QUEUE_BUSINESS, 
        Connection = "CoreEventHubConnectionString",
        ConsumerGroup = "%ConsumerGroup%",
        IsBatched = false)] string myEventHubMessage, FunctionContext context)
    {
        IMessageHandoffService messageService = context
            .InstanceServices
            .GetService<IMessageHandoffService>() ?? throw new NotImplementedException();

        await messageService.ProcessMessage(Constants.EVENT_QUEUE_BUSINESS, myEventHubMessage);
    }
}