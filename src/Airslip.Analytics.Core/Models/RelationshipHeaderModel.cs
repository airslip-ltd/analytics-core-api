using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Enums;
using Airslip.Common.Repository.Types.Entities;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models;

public class RelationshipHeaderModel : IModelWithOwnership, IModelWithTimeStamp, IFromDataSource
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string? UserId { get; set; }
    public string? EntityId { get; set; }
    public AirslipUserType AirslipUserType { get; set; }
    public DataSources DataSource { get; set; }
    public RelationshipStatus RelationshipStatus { get; set; }
    public long TimeStamp { get; set; }
    public List<RelationshipDetail> Details { get; set; } = new();
}