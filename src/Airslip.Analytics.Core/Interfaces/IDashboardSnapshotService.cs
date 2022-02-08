using Airslip.Common.Types.Interfaces;
using System.Threading.Tasks;

namespace Airslip.Analytics.Core.Interfaces;

public interface IDashboardSnapshotService
{
    Task<IResponse> GetSnapshotFor(DashboardSnapshotType dashboardSnapshotType, int dayRange, int statRange, string? accountId);
}

public enum DashboardSnapshotType
{
    TotalSales,
    TotalRefunds,
    CurrentBalance
}