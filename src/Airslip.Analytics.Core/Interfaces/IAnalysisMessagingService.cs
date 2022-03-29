using Airslip.Analytics.Core.Models;
using System.Threading.Tasks;

namespace Airslip.Analytics.Core.Interfaces;

public interface IAnalysisMessagingService
{
    Task BankAccountBalanceAnalysis(BankAccountBalanceModel model);
    Task MerchantTransactionAnalysis(MerchantTransactionModel model);
    Task BankTransactionAnalysis(BankTransactionModel model);
}