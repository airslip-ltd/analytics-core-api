using Airslip.Common.Repository.Types.Interfaces;
using JetBrains.Annotations;

namespace Airslip.Analytics.Core.Entities;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class CountryCode : IEntityWithId
{
    public string Id { get; set; } = string.Empty;
}