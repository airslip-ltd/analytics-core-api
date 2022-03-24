using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Reports.Common;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Reports.Interfaces;

public interface IDownloadService
{
    Task<IResponse> Download(IReport report, OwnedDataSearchModel query, string fileName);
}