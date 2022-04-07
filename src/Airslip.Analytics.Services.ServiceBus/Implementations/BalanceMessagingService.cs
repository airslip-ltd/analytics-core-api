using Airslip.Analytics.Core.Data;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Utilities;
using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

namespace Airslip.Analytics.Services.ServiceBus.Implementations;

public class BalanceMessagingService : IAnalysisMessagingService<BankAccountBalanceModel>
{
    private readonly ServiceBusSender _bankAccountBalanceQueue;
    private readonly ServiceBusSender _bankAccountBalanceEntityQueue;

    public BalanceMessagingService(ServiceBusClient serviceBusClient)
    {
        _bankAccountBalanceQueue = serviceBusClient.CreateSender(Constants.MESSAGE_QUEUE_BANK_ACCOUNT_BALANCE);
        _bankAccountBalanceEntityQueue = serviceBusClient.CreateSender(Constants.MESSAGE_QUEUE_BANK_ACCOUNT_BALANCE_ENTITY);
    }

    public async Task RequestAnalysis(BankAccountBalanceModel model)
    {
        ServiceBusMessage recordMessage = new(Json.Serialize(model))
        {
            MessageId = model.Id
        };
        await _bankAccountBalanceQueue.SendMessageAsync(recordMessage);

        BalanceAnalysisModel entityAnalysisModel = new(model.EntityId!, model.AirslipUserType);
        ServiceBusMessage entityMessage = new(Json.Serialize(entityAnalysisModel))
        {
            MessageId = entityAnalysisModel.EntityId,
            ScheduledEnqueueTime = DateTimeOffset.Now.AddSeconds(30)
        };
        await _bankAccountBalanceEntityQueue.SendMessageAsync(entityMessage);
    }
}