using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Integrations.Banking.Types.Enums;
using JetBrains.Annotations;

namespace Airslip.Analytics.Core.Entities
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class IntegrationAccountDetail : IEntityWithId, IReportableWithIntegration
    {
        public string Id { get; set; } = string.Empty;
        public BankingAccountStatus AccountStatus { get; set; }
        public string AccountId { get; set; } = string.Empty;
        public string? LastCardDigits { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;
        public BankingUsageTypes UsageType { get; set; }
        public BankingAccountTypes AccountType { get; set; }
        public string? SortCode { get; set; }
        public string? AccountNumber { get; set; }
        public string IntegrationId { get; set; } = string.Empty;
        public virtual Integration Integration { get; set; } = null!;
    }
}