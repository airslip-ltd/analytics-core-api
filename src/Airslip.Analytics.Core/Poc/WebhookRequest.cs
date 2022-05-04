using Airslip.Analytics.Core.Data;
using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Poc;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public record WebhookRequest
{
    /// <summary>
    /// The URL of the webhook endpoint
    /// </summary>
    public string Url { get; set; } = string.Empty;
    /// <summary>
    /// The list of events to enable for this endpoint
    /// </summary>
    public IEnumerable<WebhookEvents>? Events { get; set; }
}