using Airslip.Analytics.Core.Enums;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Integrations.Banking.Types.Enums;

namespace Airslip.Analytics.Reports.Models;

public class AccountBalanceReportModel : IModel
{
    /// <summary>
    /// A description about the property should go here
    /// </summary>
    public string? Id { get; set; }
    
    /// <summary>
    /// A description about the property should go here
    /// </summary>
    public string IntegrationProviderId { get; set; } = string.Empty;
    
    /// <summary>
    /// A description about the property should go here
    /// </summary>
    public BankingAccountStatus AccountStatus { get; set; }
    
    /// <summary>
    /// A description about the property should go here
    /// </summary>
    public string? SortCode { get; set; }
    
    /// <summary>
    /// A description about the property should go here
    /// </summary>
    public string? AccountNumber { get; set; }
    
    /// <summary>
    /// A description about the property should go here
    /// </summary>
    public string CurrencyCode { get; set; } = string.Empty;
    
    /// <summary>
    /// A description about the property should go here
    /// </summary>
    public double Balance { get; set; }
    
    /// <summary>
    /// A description about the property should go here
    /// </summary>
    public DateTime UpdatedOn { get; set; }
    
    /// <summary>
    /// A description about the property should go here
    /// </summary>
    public EntityStatus EntityStatus { get; set; }
}