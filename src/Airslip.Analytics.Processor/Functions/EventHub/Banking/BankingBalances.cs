using Airslip.Common.Services.Handoff.Interfaces;
using Airslip.Integrations.Banking.Types.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Airslip.Analytics.Processor.Functions.EventHub.Banking;

public static class BankingBalances
{
    [Function(nameof(BankingBalances))]
    public static async Task Run([EventHubTrigger(Constants.EVENT_HUB_BANKING_BALANCES, 
        Connection = "CoreEventHubConnectionString",
        ConsumerGroup = "%ConsumerGroup%",
        IsBatched = false)] string myEventHubMessage, FunctionContext context)
    {
        IMessageHandoffService messageService = context
            .InstanceServices
            .GetService<IMessageHandoffService>() ?? throw new NotImplementedException();

        await messageService.ProcessMessage(Constants.EVENT_HUB_BANKING_BALANCES, myEventHubMessage);
    }
}