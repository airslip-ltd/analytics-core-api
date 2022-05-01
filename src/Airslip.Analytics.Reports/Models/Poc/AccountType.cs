using Airslip.Analytics.Reports.Enums;

namespace Airslip.Analytics.Reports.Models.Poc;

public class AccountType<T> where T : class
{
    /// <summary>
    /// The type of account
    /// </summary>
    public AccountingAccountTypes Type { get; set; }

    /// <summary>
    /// All associated accounts
    /// </summary>
    public List<T> Accounts { get; set; } = new();
    /// <summary>
    /// Total value of all the accounts in this type
    /// </summary>
    public long Total { get; set; }
}