using Airslip.Common.Types.Interfaces;
using JetBrains.Annotations;

namespace Airslip.Analytics.Reports.Models;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class BankTransactionsReportModel : ISuccess
{
    public IEnumerable<BankTransactionReportModel> BankTransactions { get; set; } = new List<BankTransactionReportModel>();
}