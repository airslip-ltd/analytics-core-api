namespace Airslip.Analytics.Reports.Models.Poc;

public interface IAccountingAccount
{
    /// <summary>
    /// Accounting code
    /// </summary>
    public string Code { get; set; }
    /// <summary>
    /// Identifier of the account
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// Account name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// The code of the report
    /// </summary>
    public string ReportingCode { get; set; }
    /// <summary>
    /// Total movement on this account
    /// </summary>
    public long Total { get; set; }
}