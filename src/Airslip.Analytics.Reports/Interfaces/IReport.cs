using Airslip.Analytics.Core.Models;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Reports.Interfaces;

public interface IReport
{
    Task<IResponse> Execute(OwnedDataSearchModel query);
    Task<IResponse> Download(OwnedDataSearchModel query);
}