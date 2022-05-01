using Airslip.Analytics.Reports.Enums;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Reports.Models.Poc;

public class CashflowModel : IModel, ISuccess
{
    /// <summary>
    /// Start date of the cashflow report
    /// </summary>
    public long StartDate { get; set; }
    
    /// <summary>
    /// End date of the cashflow report
    /// </summary>
    public long EndDate { get; set; }

    /// <summary>
    /// Cash balance
    /// </summary>
    public CashBalance CashBalance { get; set; } = new();
    
    /// <summary>
    /// Break down of cash and cash equivalents for the period
    /// </summary>
    public List<CashflowActivity> CashflowActivities { get; set; } = new();
    
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
}

public class CashBalance
{
    /// <summary>
    /// Opening balance of cash and cash equivalents
    /// </summary>
    public long OpeningCashBalance { get; set; }
    /// <summary>
    /// Closing balance of cash and cash equivalents
    /// </summary>
    public long ClosingCashBalance { get; set; }
    /// <summary>
    /// Net movement of cash and cash equivalents for the period
    /// </summary>
    public long NetCashMovement { get; set; }
}

public class CashflowType
{
    /// <summary>
    /// Name of the activity
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Total value of the activity
    /// </summary>
    public long Total { get; set; }
    /// <summary>
    /// List of the accounts in this activity
    /// </summary>
    public List<CashflowAccount> Accounts { get; set; } = new();
    
    public class CashflowAccount : IAccountingAccount
    {
        /// <summary>
        /// Identifier of the account
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The type of account
        /// </summary>
        public AccountingAccountTypes Type { get; set; }
        /// <summary>
        /// The class type of the account
        /// </summary>
        public AccountingAccountClassTypes ClassType { get; set; }
        /// <summary>
        /// Account code
        /// </summary>
        public string Code { get; set; }= string.Empty;
        /// <summary>
        /// Account name
        /// </summary>
        public string Name { get; set; }= string.Empty;
        /// <summary>
        /// Reporting code used for cash flow classification
        /// </summary>
        public string ReportingCode { get; set; }= string.Empty;
        /// <summary>
        /// Total amount for the account
        /// </summary>
        public long Total { get; set; }
    }
}

public class CashflowActivity
{
    /// <summary>
    /// Name of the cashflow activity type
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Total value of the activity type
    /// </summary>
    public long Total { get; set; }
    /// <summary>
    /// All cash flow types
    /// </summary>
    public List<CashflowType> CashflowTypes { get; set; }
}
