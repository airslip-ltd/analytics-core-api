using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Core.Models.Raw.CustomerPortal;

public class RawBusinessModel : IFromDataSource
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? PrimaryUserId { get; set; }
    public DataSources DataSource { get; set; }
    public long TimeStamp { get; set; }
}