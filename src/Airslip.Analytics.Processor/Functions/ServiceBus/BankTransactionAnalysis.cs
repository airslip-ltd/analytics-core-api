using Airslip.Analytics.Core.Constants;
using Airslip.Common.Services.Handoff.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Airslip.Analytics.Processor.Functions.ServiceBus
{
    public static class BankTransactionAnalysis
    {
        [Function("BankTransactionAnalysis")]
        public static async Task Run([ServiceBusTrigger(Constants.MESSAGE_QUEUE_BANK_TRANSACTION, 
                Connection = "ServiceBusConnectionString")] 
            string message, FunctionContext context)
        {
            IMessageHandoffService messageService = context
                .InstanceServices
                .GetService<IMessageHandoffService>() ?? throw new NotImplementedException();

            await messageService.ProcessMessage(Constants.MESSAGE_QUEUE_BANK_TRANSACTION, message);
        }
    }
}