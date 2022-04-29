using Airslip.Analytics.Core.Models;
using Airslip.Common.CustomerPortal.Enums;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Reports.Models;

public record IntegrationReportModel
{
    public string? Id { get; set; }
    public string Name { get; init; } = string.Empty;
    public IntegrationAccountDetailReportModel? AccountDetail { get; set; }
    public IntegrationProviderReportModel? Provider { get; set; }
}