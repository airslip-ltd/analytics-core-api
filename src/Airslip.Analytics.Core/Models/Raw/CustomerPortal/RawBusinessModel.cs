using Airslip.Common.Repository.Types.Enums;

namespace Airslip.Analytics.Core.Models.Raw.CustomerPortal;

public class RawBusinessModel
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? PrimaryUserId { get; set; }
    public long TimeStamp { get; set; }
}