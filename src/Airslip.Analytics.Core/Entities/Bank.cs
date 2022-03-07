using Airslip.Analytics.Core.Enums;
using Airslip.Common.Repository.Types.Entities;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;
using Airslip.Common.Utilities.Extensions;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Entities;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class Bank : IEntity, IFromDataSource
{
    public string Id { get; set; } = string.Empty;
    public virtual BasicAuditInformation? AuditInformation { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string TradingName { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public EnvironmentType EnvironmentType { get; set; } = EnvironmentType.Sandbox;
    public virtual ICollection<BankCountryCode> CountryCodes { get; set; } = new List<BankCountryCode>();
    public DataSources DataSource { get; set; } = DataSources.Unknown;
    public long TimeStamp { get; set; } = DateTime.UtcNow.ToUnixTimeMilliseconds();
}