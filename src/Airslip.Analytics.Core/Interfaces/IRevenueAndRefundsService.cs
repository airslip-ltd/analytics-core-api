using Airslip.Common.Types.Interfaces;
using System.Threading.Tasks;

namespace Airslip.Analytics.Core.Interfaces;

public interface IRevenueAndRefundsService
{
    Task<IResponse> GetRevenueAndRefunds(int year, string? integrationId);
}