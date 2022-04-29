using Airslip.Common.Repository.Types.Entities;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;
using Airslip.Common.Utilities.Extensions;
using System;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Entities;

public record IntegrationProvider : IEntity, IFromDataSource
{
    public string Id { get; set; } = string.Empty;
    public virtual BasicAuditInformation? AuditInformation { get; set; }
    public EntityStatus EntityStatus { get; set; } = EntityStatus.Active;
    public string Name { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public string FriendlyName { get; set; } = string.Empty;
    public IntegrationType IntegrationType { get; set; } = IntegrationType.Commerce;
    public EnvironmentType EnvironmentType { get; set; } = EnvironmentType.Live;
    public string Integration { get; set; } = string.Empty;
    public long TimeStamp { get; set; } = DateTime.UtcNow.ToUnixTimeMilliseconds();
    public DataSources DataSource { get; set; } = DataSources.Unknown;
    public virtual ICollection<Integration> Integrations { get; set; } = new List<Integration>();
}