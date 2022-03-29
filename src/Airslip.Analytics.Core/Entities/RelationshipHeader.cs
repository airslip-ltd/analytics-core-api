using Airslip.Analytics.Core.Enums;
using Airslip.Common.Repository.Types.Entities;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Entities;

public class RelationshipHeader : IEntityWithOwnership, IFromDataSource, IEntityWithTimeStamp
{
    public string Id { get; set; }
    public virtual BasicAuditInformation? AuditInformation { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public DataSources DataSource { get; set; }
    public string? UserId { get; set; }
    public string? EntityId { get; set; }
    public AirslipUserType AirslipUserType { get; set; }
    public RelationshipStatus RelationshipStatus { get; set; }
    public long TimeStamp { get; set; }
    public virtual ICollection<RelationshipDetail> Details { get; set; } = new List<RelationshipDetail>();
}