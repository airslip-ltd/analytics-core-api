using Airslip.Analytics.Reports.Enums;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Reports.Models.Poc;

public class CashPositionModel : IModel, ISuccess
{
    /// <summary>
    /// The Airslip identifier for an account
    /// </summary>
    public string AccountId { get; set; } = string.Empty;
    /// <summary>
    /// The current state of the statement balance
    /// </summary>
    public StatementBalance StatementBalance { get; set; } = new();
    /// <summary>
    /// The date when the last bank statement item was entered into the accounting system
    /// </summary>
    public long StatementBalanceDate { get; set; }
    /// <summary>
    /// The bank statement
    /// </summary>
    public BankStatement BankStatement { get; set; } = new();
    /// <summary>
    /// Details relating to the cash account
    /// </summary>
    public CashAccount CashAccount { get; set; } = new();

    public EntityStatus EntityStatus { get; set; } // Ignore
    public string? Id { get; set; }
}

public class StatementBalance
{
    /// <summary>
    /// Total closing balance of the account. This includes both reconciled and unreconciled bank statement lines. The closing balance will always be represented as a positive number, with it’s debit/credit status defined in the statementBalanceDebitCredit field
    /// </summary>
    public long Value { get; set; }
    /// <summary>
    /// The debit or credit status of the account. Cash accounts in credit have a negative balance
    /// </summary>
    public TransactionFlows Type { get; set; }
}

public class DataSource
{
    /// <summary>
    /// The data source type
    /// </summary>
    public ImportSourceTypes Type { get; set; }
    
    /// <summary>
    /// Sum of the positive amounts of all statement lines where the source of the data was equal to the type
    /// </summary>
    public int PositiveTransactionCount { get; set; }

    /// <summary>
    /// Sum of the negative amounts of all statement lines where the source of the data was equal to the type
    /// </summary>
    public int NegativeTransactionCount { get; set; }
}

public class StatementLines
{
    /// <summary>
    /// Sum of the amounts of all statement lines where both the reconciled flag is set to false, and the amount is positive
    /// </summary>
    public long UnreconciledAmountPos { get; set; }
    /// <summary>
    /// Sum of the amounts of all statement lines where both the reconciled flag is set to false, and the amount is negative
    /// </summary>
    public long UnreconciledAmountNeg { get; set; }
    /// <summary>
    /// Sum of the amounts of all statement lines where both the reconciled flag is set to FALSE, and the amount is negative
    /// </summary>
    public int UnreconciledLines { get; set; }
    /// <summary>
    /// Sum-product of age of statement line in days multiplied by transaction amount, divided by the sum of transaction amount - in for those statement lines in which the reconciled flag is set to false, and the amount is positive. Provides an indication of the age of unreconciled transactions
    /// </summary>
    public double AveragePositiveDaysUnreconciled { get; set; }
    /// <summary>
    /// Sum-product of age of statement line in days multiplied by transaction amount, divided by the sum of transaction amount - in for those statement lines in which the reconciled flag is set to FALSE, and the amount is negative. Provides an indication of the age of unreconciled transactions
    /// </summary>
    public double AverageNegativeDaysUnreconciled { get; set; }
    /// <summary>
    /// The date which is the earliest transaction date of a statement line for which the reconciled flag is set to false
    /// </summary>
    public long EarliestUnreconciledTransaction { get; set; }
    /// <summary>
    /// The date which is the latest transaction date of a statement line for which the reconciled flag is set to FALSE
    /// </summary>
    public long LatestUnreconciledTransaction { get; set; }
    /// <summary>
    /// Sum of the amounts of all deleted statement lines. Transactions may be deleted due to duplication or otherwise
    /// </summary>
    public int DeletedAmount { get; set; }
    /// <summary>
    /// Sum of the amounts of all statement lines. This is used as a metric of comparison to the unreconciled figures above
    /// </summary>
    public int TotalAmount { get; set; }
    
    /// <summary>
    /// Metrics to give an indication on the certainty of correctness of the data
    /// </summary>
    public List<DataSource> DataSources { get; set; } = new();
    /// <summary>
    /// The date which is the earliest transaction date of a statement line for which the reconciled flag is set to TRUE
    /// </summary>
    public long EarliestReconciledTransaction { get; set; }
    /// <summary>
    /// The date which is the latest transaction date of a statement line for which the reconciled flag is set to TRUE
    /// </summary>
    public long LatestReconciledTransaction { get; set; }
    /// <summary>
    /// Sum of the amounts of all statement lines where both the reconciled flag is set to TRUE, and the amount is positive
    /// </summary>
    public int ReconciledPositiveAmount { get; set; }
    /// <summary>
    /// Sum of the amounts of all statement lines where both the reconciled flag is set to TRUE, and the amount is negative
    /// </summary>
    public int ReconciledNegativeAmount { get; set; }
    /// <summary>
    /// Count of all statement lines where the reconciled flag is set to TRUE
    /// </summary>
    public int ReconciledLines { get; set; }
    /// <summary>
    /// Sum of the amounts of all statement lines where the amount is positive
    /// </summary>
    public int TotalPositiveAmount { get; set; }
    /// <summary>
    /// Sum of the amounts of all statement lines where the amount is negative
    /// </summary>
    public int TotalNegativeAmount { get; set; }
}

public class CurrentStatement
{
    /// <summary>
    /// Looking at the most recent bank statement, this field indicates the first date which transactions on this statement pertain to
    /// </summary>
    public long StartDate { get; set; }
    /// <summary>
    /// Looking at the most recent bank statement, this field indicates the last date which transactions on this statement pertain to
    /// </summary>
    public long EndDate { get; set; }
    /// <summary>
    /// Looking at the most recent bank statement, this field indicates the balance before the transactions on the statement are applied (note, this is not always populated by the bank in every single instance (~10%))
    /// </summary>
    public long StartBalance { get; set; }
    /// <summary>
    /// Looking at the most recent bank statement, this field indicates the balance after the transactions on the statement are applied (note, this is not always populated by the bank in every single instance (~10%)).
    /// </summary>
    public long EndBalance { get; set; }
    /// <summary>
    /// Looking at the most recent bank statement, this field indicates when the document was imported
    /// </summary>
    public long ImportedDateTime { get; set; }
    /// <summary>
    /// Looking at the most recent bank statement, this field indicates the source of the data
    /// </summary>
    public ImportSourceTypes ImportSourceType { get; set; }
}

public class BankStatement
{
    public StatementLines StatementLines { get; set; } = new();
    public CurrentStatement CurrentStatement { get; set; } = new();
}

public class CashAccount
{
    /// <summary>
    /// Total value of transactions in the journals which are not reconciled to bank statement lines, and have a positive (debit) value.
    /// </summary>
    public int UnreconciledPositionAmount { get; set; }
    /// <summary>
    /// Total value of transactions in the journals which are not reconciled to bank statement lines, and have a negative (credit) value.
    /// </summary>
    public int UnreconciledNegativeAmount { get; set; }
    /// <summary>
    /// Starting (or historic) balance from the journals (manually keyed in by users on account creation - unverified).
    /// </summary>
    public long StartingBalance { get; set; }
    /// <summary>
    /// Current cash at bank accounting value from the journals.
    /// </summary>
    public long AccountBalance { get; set; }
    /// <summary>
    /// Currency which the cash account transactions relate to.
    /// </summary>
    public Iso4217CurrencyCodes BalanceCurrency { get; set; }
}