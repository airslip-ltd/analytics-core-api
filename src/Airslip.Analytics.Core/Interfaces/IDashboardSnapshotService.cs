using Airslip.Analytics.Core.Models;
using Airslip.Common.Types.Interfaces;
using System.Threading.Tasks;

namespace Airslip.Analytics.Core.Interfaces;

public interface IDashboardSnapshotService
{
    Task<IResponse> GetSnapshotFor(OwnedSnapshotSearchModel query, DashboardSnapshotType dashboardSnapshotType, int dayRange, int statRange, string? integrationId);
}

public enum DashboardSnapshotType
{
    TotalSales,
    TotalRefunds,
    CurrentBalance
}