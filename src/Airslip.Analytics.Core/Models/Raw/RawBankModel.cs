using Airslip.Analytics.Core.Enums;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Utilities.Extensions;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models.Raw;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public record RawBankModel
{
    public string? Id { get; set; }
    public string TradingName { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public string EnvironmentType { get; set; } = string.Empty;
    public List<string> CountryCodes { get; set; } = new();
    public EntityStatus EntityStatus { get; set; }
    public DataSources DataSource { get; set; } = DataSources.Unknown;
    public long TimeStamp { get; set; } = DateTime.UtcNow.ToUnixTimeMilliseconds();
}