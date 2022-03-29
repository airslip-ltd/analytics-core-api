using Airslip.Analytics.Core.Constants;
using Airslip.Common.Services.Handoff.Interfaces;
using JetBrains.Annotations;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Airslip.Analytics.Processor.Functions.EventHub.Transactions;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public static class MerchantTransactions
{
    [Function(nameof(MerchantTransactions))]
    public static async Task Run([EventHubTrigger(Constants.EVENT_QUEUE_MERCHANT_TRANSACTIONS, 
        Connection = "TransactionEventHubConnectionString",
        ConsumerGroup = "%ConsumerGroup%",
        IsBatched = false)] string myEventHubMessage, FunctionContext context)
    {
        IMessageHandoffService messageService = context
            .InstanceServices
            .GetService<IMessageHandoffService>() ?? throw new NotImplementedException();

        await messageService.ProcessMessage(Constants.EVENT_QUEUE_MERCHANT_TRANSACTIONS, myEventHubMessage);
    }
}