using Airslip.Common.Types.Enums;

namespace Airslip.Analytics.Reports.Models;

public record IntegrationProviderReportModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public string FriendlyName { get; set; } = string.Empty;
    public IntegrationType IntegrationType { get; set; } = IntegrationType.Commerce;
}