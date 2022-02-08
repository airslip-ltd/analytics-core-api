using Airslip.Common.Types.Interfaces;
using System.Threading.Tasks;

namespace Airslip.Analytics.Core.Interfaces;

public interface ITransactionService
{
    Task<IResponse> GetBankingTransactions(int limit, string? accountId);
    Task<IResponse> GetMerchantTransactions(int limit, string? accountId);
}