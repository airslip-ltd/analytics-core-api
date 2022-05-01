using Airslip.Analytics.Reports.Enums;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Reports.Models.Poc;

public class ProfitLossModel : IModel, ISuccess
{
    /// <summary>
    /// Start date of the profit and loss report
    /// </summary>
    public long StartDate { get; set; }
    
    /// <summary>
    /// End date of the profit and loss report
    /// </summary>
    public long EndDate { get; set; }
    
    /// <summary>
    /// Net profit loss value
    /// </summary>
    public long NetProfitLoss { get; set; }

    /// <summary>
    /// Revenue and expense reports
    /// </summary>
    public List<ProfitLossReport> Reports { get; set; } = new();

    public EntityStatus EntityStatus { get; set; } // Ignore
    public string? Id { get; set; }
}

public class ProfitLossReport 
{
    /// <summary>
    /// Revenue or expense report
    /// </summary>
    public ProfitLossTypes ProfitLossType { get; set; }

    /// <summary>
    /// All account types associated to this profit and loss report
    /// </summary>
    public List<AccountType<ProfitLossAccount>> AccountTypes { get; set; } = new();
    /// <summary>
    /// The total amount of all accounts
    /// </summary>
    public long Total { get; set; }
}

/// <summary>
/// A list of all accounts of this type
/// </summary>
public class ProfitLossAccount : IAccountingAccount
{
    /// <summary>
    /// Accounting code
    /// </summary>
    public string Code { get; set; } = string.Empty;
    /// <summary>
    /// Identifier of the account
    /// </summary>
    public string Id { get; set; } = string.Empty;
    /// <summary>
    /// Account name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// The code of the report
    /// </summary>
    public string ReportingCode { get; set; } = string.Empty;
    /// <summary>
    /// Total movement on this account
    /// </summary>
    public long Total { get; set; }
}