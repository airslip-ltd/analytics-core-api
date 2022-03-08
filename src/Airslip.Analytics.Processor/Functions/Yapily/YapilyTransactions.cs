// using Airslip.Analytics.Core.Constants;
// using Airslip.Common.Services.Handoff.Interfaces;
// using Microsoft.Azure.Functions.Worker;
// using Microsoft.Extensions.DependencyInjection;
// using System;
// using System.Threading.Tasks;
//
// namespace Airslip.Analytics.Processor.Functions.Yapily
// {
//     public static class YapilyTransactions
//     {
//         [Function(nameof(YapilyTransactions))]
//         public static async Task Run([EventHubTrigger(Constants.EVENT_QUEUE_YAPILY_TRANSACTIONS, 
//             Connection = "YapilyEventHubConnectionString",
//             ConsumerGroup = "%ConsumerGroup%",
//             IsBatched = false)] string myEventHubMessage, FunctionContext context)
//         {
//             IMessageHandoffService messageService = context
//                 .InstanceServices
//                 .GetService<IMessageHandoffService>() ?? throw new NotImplementedException();
//
//             await messageService.ProcessMessage(Constants.EVENT_QUEUE_YAPILY_TRANSACTIONS, myEventHubMessage);
//         }
//     }
// }