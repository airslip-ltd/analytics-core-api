// using Airslip.Analytics.Core.Constants;
// using Airslip.Common.Services.Handoff.Interfaces;
// using Microsoft.Azure.Functions.Worker;
// using Microsoft.Extensions.DependencyInjection;
// using System;
// using System.Threading.Tasks;
//
// namespace Airslip.Analytics.Processor.Functions.Api2Cart;
//
// public static class Api2CartAccounts
// {
//     [Function(nameof(Api2CartAccounts))]
//     public static async Task Run([EventHubTrigger(Constants.EVENT_QUEUE_API_2_CART_ACCOUNTS, 
//         Connection = "Api2CartEventHubConnectionString",
//         ConsumerGroup = "%ConsumerGroup%",
//         IsBatched = false)] string myEventHubMessage, FunctionContext context)
//     {
//         IMessageHandoffService messageService = context
//             .InstanceServices
//             .GetService<IMessageHandoffService>() ?? throw new NotImplementedException();
//
//         await messageService.ProcessMessage(Constants.EVENT_QUEUE_API_2_CART_ACCOUNTS, myEventHubMessage);
//     }
// }