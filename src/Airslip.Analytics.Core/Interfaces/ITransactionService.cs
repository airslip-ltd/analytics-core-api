using Airslip.Common.Types.Interfaces;
using System.Threading.Tasks;

namespace Airslip.Analytics.Core.Interfaces;

public interface ITransactionService
{
    Task<IResponse> GetAccountTransactions(int limit, string? accountId);
}