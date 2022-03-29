using Airslip.Analytics.Core.Constants;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Utilities;
using Azure.Messaging.ServiceBus;
using System.Threading.Tasks;

namespace Airslip.Analytics.Services.ServiceBus.Implementations
{
    public class CommerceMessagingService : IAnalysisMessagingService<MerchantTransactionModel>
    {
        private readonly ServiceBusSender _merchantTransactionQueue;

        public CommerceMessagingService(ServiceBusClient serviceBusClient)
        {
            _merchantTransactionQueue = serviceBusClient.CreateSender(Constants.MESSAGE_QUEUE_MERCHANT_TRANSACTION);
        }

        public async Task RequestAnalysis(MerchantTransactionModel model)
        {
            ServiceBusMessage message = new(Json.Serialize(model))
            {
                MessageId = model.Id
            };
            await _merchantTransactionQueue.SendMessageAsync(message);
        }
    }
}