using Airslip.Analytics.Core.Enums;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;

namespace Airslip.Analytics.Reports.Models;

public class AccountBalanceReportModel : IModel
{
    public string? Id { get; set; }
    public string IntegrationProviderId { get; set; } = string.Empty;
    public AccountStatus AccountStatus { get; set; }
    public string? SortCode { get; set; }
    public string? AccountNumber { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public double Balance { get; set; }
    public DateTime UpdatedOn { get; set; }
    public EntityStatus EntityStatus { get; set; }
}