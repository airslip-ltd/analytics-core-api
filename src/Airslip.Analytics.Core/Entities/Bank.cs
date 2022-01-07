using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Repository.Types.Entities;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Utilities.Extensions;
using System;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Entities
{
    public class Bank : IEntity, IFromDataSource
    {
        public string Id { get; set; } = string.Empty;
        public virtual BasicAuditInformation? AuditInformation { get; set; }
        public EntityStatus EntityStatus { get; set; }
        public string TradingName { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public string EnvironmentType { get; set; } = string.Empty;
        public virtual ICollection<BankCountryCode> CountryCodes { get; set; } = new List<BankCountryCode>();
        public DataSources DataSource { get; set; } = DataSources.Unknown;
        public long TimeStamp { get; set; } = DateTime.UtcNow.ToUnixTimeMilliseconds();
    }
}