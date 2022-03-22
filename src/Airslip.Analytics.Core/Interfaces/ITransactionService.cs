using Airslip.Common.Types.Interfaces;
using System.Threading.Tasks;

namespace Airslip.Analytics.Core.Interfaces;

public interface ITransactionService
{
    Task<IResponse> GetBankingTransactions(int limit, string? integrationId);
    Task<IResponse> GetCommerceTransactions(int limit, string? integrationId);
    Task<IResponse> GetMerchantAccounts();
}