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
public record WebhookModel : IModel, ISuccess
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    
    public string Object { get; set; }
    public object ApiVersion { get; set; }
    public object Application { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long Created { get; set; }
    public string Description { get; set; }
    public List<WebhookEvents> EnabledEvents { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public WebhookStatus Status { get; set; }
    /// <summary>
    /// The URL of the webhook endpoint.
    /// </summary>
    public string Url { get; set; }
    /// <summary>
    /// The endpoint’s secret, used to generate webhook signatures. Only returned at creation.
    /// </summary>
    public string Secret { get; set; }
}