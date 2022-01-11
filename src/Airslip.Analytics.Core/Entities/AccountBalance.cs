﻿using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Repository.Types.Entities;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Utilities.Extensions;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Entities;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public record AccountBalance : IEntityWithOwnership, IFromDataSource
{
    public string Id { get; set; } = string.Empty;
    public virtual BasicAuditInformation? AuditInformation { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string? UserId { get; set; }
    public string? EntityId { get; set; }
    public AirslipUserType AirslipUserType { get; set; }
    public string AccountId { get; set; } = string.Empty;
    public BalanceStatus BalanceStatus { get; init; }
    public long Balance { get; init; }
    public string? Currency { get; init; }
    public virtual ICollection<AccountBalanceDetail> Details { get; init; } = new List<AccountBalanceDetail>();
    public DataSources DataSource { get; set; } = DataSources.Unknown;
    public long TimeStamp { get; set; } = DateTime.UtcNow.ToUnixTimeMilliseconds();
}