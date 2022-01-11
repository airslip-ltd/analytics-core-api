using JetBrains.Annotations;

namespace Airslip.Analytics.Core.Entities;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class BankCountryCode
{
    public string Id { get; set; } = string.Empty;
    public string BankId { get; set; } = string.Empty;
}