using Airslip.Analytics.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models;

public class MerchantRefundModel : IModelWithId
{
    public string Id { get; set; } = string.Empty;
    public long? Shipping { get; set; }
    public long? Fee { get; set; }
    public long? Tax { get; set; }
    public long? Total { get; set; }
    public DateTime ModifiedTime { get; set; }
    public string? Comment { get; set; }
    public List<MerchantRefundItemModel>? Items { get; set; }
}