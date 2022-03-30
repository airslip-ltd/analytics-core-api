using Airslip.Analytics.Core.Models;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Reports.Interfaces;

public interface IDownloadService
{
    Task<IResponse> Download<TResponseType>(IReport report, OwnedDataSearchModel query, string fileName);
}