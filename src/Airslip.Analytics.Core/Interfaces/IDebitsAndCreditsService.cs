using Airslip.Analytics.Core.Models;
using Airslip.Common.Types.Interfaces;
using System.Threading.Tasks;

namespace Airslip.Analytics.Core.Interfaces;

public interface IDebitsAndCreditsService
{
    Task<IResponse> GetDebitsAndCredits(OwnedSnapshotSearchModel query, int year, string? integrationId);
}