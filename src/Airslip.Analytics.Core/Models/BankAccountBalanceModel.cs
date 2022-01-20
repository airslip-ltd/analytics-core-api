using Airslip.Analytics.Core.Enums;
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
public record BankAccountBalanceModel : IModelWithOwnership, IFromDataSource
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string? UserId { get; set; }
    public string? EntityId { get; set; }
    public AirslipUserType AirslipUserType { get; set; }
    public string AccountId { get; set; } = string.Empty;
    public BalanceStatus BalanceStatus { get; init; }
    public long Balance { get; init; }
    public string? Currency { get; init; }
    public List<BankAccountBalanceDetailModel> Details { get; init; } = new();
    public DataSources DataSource { get; set; } = DataSources.Unknown;
    public long TimeStamp { get; set; }
}