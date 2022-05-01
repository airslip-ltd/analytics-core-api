using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Reports.Models.Poc;

public class CreatedModel : IModelWithId, ISuccess
{
    public string? Id { get; set; }
}