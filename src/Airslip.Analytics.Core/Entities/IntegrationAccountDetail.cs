using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Repository.Types.Interfaces;
using JetBrains.Annotations;

namespace Airslip.Analytics.Core.Entities
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class IntegrationAccountDetail : IEntityWithId, IReportableWithAccount
    {
        public string Id { get; set; } = string.Empty;
        public AccountStatus AccountStatus { get; set; }
        public string AccountId { get; set; } = string.Empty;
        public string? LastCardDigits { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;
        public string UsageType { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public string? SortCode { get; set; }
        public string? AccountNumber { get; set; }
        public string IntegrationId { get; set; } = string.Empty;
        public virtual Integration Integration { get; set; } = null!;
    }
}