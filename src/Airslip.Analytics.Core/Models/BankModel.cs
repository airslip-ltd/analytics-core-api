using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;
using Airslip.Common.Utilities.Extensions;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public record BankModel : IModel, IFromDataSource
{
    public string? Id { get; set; }
    public string TradingName { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public string EnvironmentType { get; set; } = string.Empty;
    public List<BankCountryCodeModel> CountryCodes { get; set; } = new();
    public EntityStatus EntityStatus { get; set; }
    public DataSources DataSource { get; set; } = DataSources.Unknown;
    public long TimeStamp { get; set; }
}

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class BankCountryCodeModel
{
    public string? Id { get; set; }
}