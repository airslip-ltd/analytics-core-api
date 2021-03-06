using Airslip.Analytics.Reports.Enums;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Reports.Models.Poc;

public class BalanceSheetModel : IModel, ISuccess
{
    /// <summary>
    /// The date of the balance sheet
    /// </summary>
    public long BalanceDate { get; set; }

    /// <summary>
    /// All balance sheet reports such as assets, liabilities and equity
    /// </summary>
    public List<BalanceSheetReport> Reports { get; set; } = new();

    public EntityStatus EntityStatus { get; set; } // Ignore
    public string? Id { get; set; }
}

public class BalanceSheetReport 
{
    /// <summary>
    /// The type of balance sheet report
    /// </summary>
    public BalanceSheetTypes BalanceSheetType { get; set; }

    /// <summary>
    /// All account types associated to this balance sheet report
    /// </summary>
    public List<AccountType<BalanceSheetAccount>> AccountTypes { get; set; } = new();
    /// <summary>
    /// The total amount of all accounts
    /// </summary>
    public long Total { get; set; }
}

/// <summary>
/// A list of all accounts of this type
/// </summary>
public class BalanceSheetAccount : IAccountingAccount
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