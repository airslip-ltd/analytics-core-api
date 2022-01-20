using Airslip.Common.Repository.Types.Interfaces;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Entities;

public class MerchantPaymentDetail : IEntityWithId
{
    public string Id { get; set; } = string.Empty;
    public string? Method { get; init; }
    public long? Amount { get; init; }
    public virtual ICollection<MerchantCardDetail> CardDetails { get; init; } = new List<MerchantCardDetail>();
}
