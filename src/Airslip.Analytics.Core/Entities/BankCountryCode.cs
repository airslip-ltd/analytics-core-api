using Airslip.Common.Repository.Types.Interfaces;
using JetBrains.Annotations;

namespace Airslip.Analytics.Core.Entities;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class BankCountryCode : IEntityWithId
{
    public string Id { get; set; } = string.Empty;
    public string BankId { get; set; } = string.Empty;
    public virtual Bank Bank { get; set; } = null!;
}