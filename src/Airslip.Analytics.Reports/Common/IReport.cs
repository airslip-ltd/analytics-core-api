using Airslip.Analytics.Core.Models;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Reports.Common;

public interface IReport
{
    Task<IResponse> Execute(OwnedDataSearchModel query);
}