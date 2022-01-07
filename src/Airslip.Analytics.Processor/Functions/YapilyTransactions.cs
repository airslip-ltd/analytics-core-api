// using Microsoft.Azure.Functions.Worker;
// using Microsoft.Extensions.DependencyInjection;
// using Serilog;
// using System;
// using System.Threading.Tasks;
//
// namespace Airslip.Analytics.Processor.Functions
// {
//     public static class YapilyTransactions
//     {
//         [Function(nameof(YapilyTransactions))]
//         public static async Task Run([EventHubTrigger("yapily-transactions", 
//             Connection = "YapilyEventHubConnectionString",
//             ConsumerGroup = "%ConsumerGroup%", 
//             IsBatched = false)] string myEventHubMessage, FunctionContext context)
//         {
//             ILogger logger = context.InstanceServices.GetService<ILogger>()!;
//             IRegisterDataService<Transaction, IncomingTransactionModel> envelopeDeliveryService = context.InstanceServices
//                 .GetService<IRegisterDataService<Transaction, IncomingTransactionModel>>()!;
//             
//             logger.Information("Triggered {TriggerName}", nameof(YapilyTransactions));
//
//             try
//             {
//                 await envelopeDeliveryService.RegisterData(myEventHubMessage, DataSources.Yapily);
//             }
//             catch (Exception ee)
//             {
//                 logger.Fatal(ee, "Uncaught error in {TriggerName}", nameof(YapilyTransactions));                
//             }
//             
//             logger.Information("Completed {TriggerName}", nameof(YapilyTransactions));
//         }
//     }
// }