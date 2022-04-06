using Airslip.Analytics.Core.Data;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Utilities;
using Azure.Messaging.ServiceBus;
using System.Threading.Tasks;

namespace Airslip.Analytics.Services.ServiceBus.Implementations
{
    public class BalanceMessagingService : IAnalysisMessagingService<BankAccountBalanceModel>
    {
        private readonly ServiceBusSender _bankAccountBalanceQueue;

        public BalanceMessagingService(ServiceBusClient serviceBusClient)
        {
            _bankAccountBalanceQueue = serviceBusClient.CreateSender(Constants.MESSAGE_QUEUE_BANK_ACCOUNT_BALANCE);
        }

        public async Task RequestAnalysis(BankAccountBalanceModel model)
        {
            ServiceBusMessage message = new(Json.Serialize(model))
            {
                MessageId = model.Id
            };
            await _bankAccountBalanceQueue.SendMessageAsync(message);
        }
    }
}