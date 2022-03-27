using Airslip.Analytics.Core.Entities;

namespace Airslip.Analytics.Reports.Data;

public class CommerceTransactionDownloadQuery : CommerceTransactionReportQuery
{
    public virtual ICollection<MerchantProduct> Products { get; init; } = new List<MerchantProduct>();
    public virtual ICollection<MerchantRefund> Refunds { get; init; } = new List<MerchantRefund>();
}