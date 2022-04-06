using Airslip.Analytics.Core.Data;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Utilities;
using Azure.Messaging.ServiceBus;
using System.Threading.Tasks;

namespace Airslip.Analytics.Services.ServiceBus.Implementations;

public class TransactionsMessagingService : IAnalysisMessagingService<BankTransactionModel>
{
    private readonly ServiceBusSender _bankTransactionQueue;

    public TransactionsMessagingService(ServiceBusClient serviceBusClient)
    {
        _bankTransactionQueue = serviceBusClient.CreateSender(Constants.MESSAGE_QUEUE_BANK_TRANSACTION);
    }

    public async Task RequestAnalysis(BankTransactionModel model)
    {
        ServiceBusMessage message = new(Json.Serialize(model))
        {
            MessageId = model.Id
        };
        await _bankTransactionQueue.SendMessageAsync(message);
    }
}