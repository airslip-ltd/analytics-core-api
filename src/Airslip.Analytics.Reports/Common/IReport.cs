using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Reports.Common;

public interface IReport
{
    Task<IResponse> Execute(EntitySearchQueryModel query);
}