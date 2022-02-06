using Airslip.Common.Types.Interfaces;
using System.Threading.Tasks;

namespace Airslip.Analytics.Core.Interfaces;

public interface IBalanceService
{
    Task<IResponse> GetAccountBalances();
}