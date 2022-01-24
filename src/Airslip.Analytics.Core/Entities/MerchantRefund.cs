using Airslip.Common.Repository.Types.Interfaces;
using System;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Entities;

public class MerchantRefund : IEntityWithId
{
    public string Id { get; set; } = string.Empty;
    public long? Shipping { get; set; }
    public long? Fee { get; set; }
    public long? Tax { get; set; }
    public long? Total { get; set; }
    public DateTime ModifiedTime { get; set; }
    public string? Comment { get; set; }
    public virtual ICollection<MerchantRefundItem>? Items { get; set; }
}