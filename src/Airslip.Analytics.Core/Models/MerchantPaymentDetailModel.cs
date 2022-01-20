using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models;

public class MerchantPaymentDetailModel : IModelWithId
{
    public string Id { get; set; } = string.Empty;
    public string? Method { get; init; }
    public long? Amount { get; init; }
    public List<MerchantCardDetailModel> CardDetails { get; init; } = new();
}