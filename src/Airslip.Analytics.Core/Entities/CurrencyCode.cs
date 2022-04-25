using Airslip.Common.Repository.Types.Entities;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;

namespace Airslip.Analytics.Core.Entities;

public class CurrencyDetail : IEntity
{
    public string Id { get; set; } = string.Empty;
    public virtual BasicAuditInformation? AuditInformation { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string DefaultCulture { get; set; } = string.Empty;
}