using Airslip.Analytics.Core.Constants;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Utilities;
using Azure.Messaging.ServiceBus;
using System.Threading.Tasks;

namespace Airslip.Analytics.Services.ServiceBus.Implementations
{
    public class AnalysisMessagingService : IAnalysisMessagingService
    {
        private readonly ServiceBusSender _bankAccountBalanceQueue;
        private readonly ServiceBusSender _merchantTransactionQueue;
        private readonly ServiceBusSender _bankTransactionQueue;

        public AnalysisMessagingService(ServiceBusClient serviceBusClient)
        {
            _bankAccountBalanceQueue = serviceBusClient.CreateSender(Constants.MESSAGE_QUEUE_BANK_ACCOUNT_BALANCE);
            _merchantTransactionQueue = serviceBusClient.CreateSender(Constants.MESSAGE_QUEUE_MERCHANT_TRANSACTION);
            _bankTransactionQueue = serviceBusClient.CreateSender(Constants.MESSAGE_QUEUE_BANK_TRANSACTION);
        }

        public async Task BankAccountBalanceAnalysis(BankAccountBalanceModel model)
        {
            ServiceBusMessage message = new(Json.Serialize(model))
            {
                MessageId = model.Id
            };
            await _bankAccountBalanceQueue.SendMessageAsync(message);
        }

        public async Task MerchantTransactionAnalysis(MerchantTransactionModel model)
        {
            ServiceBusMessage message = new(Json.Serialize(model))
            {
                MessageId = model.Id
            };
            await _merchantTransactionQueue.SendMessageAsync(message);
        }

        public async Task BankTransactionAnalysis(BankTransactionModel model)
        {
            ServiceBusMessage message = new(Json.Serialize(model))
            {
                MessageId = model.Id
            };
            await _bankTransactionQueue.SendMessageAsync(message);
        }
    }
}