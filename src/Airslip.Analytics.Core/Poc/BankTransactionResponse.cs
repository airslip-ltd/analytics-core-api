using Airslip.Common.Types.Interfaces;
using JetBrains.Annotations;

namespace Airslip.Analytics.Core.Poc;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class BankTransactionResponse : ISuccess
{
    public string? Id { get; set; }
    public string BankTransactionId { get; set; } = string.Empty;
    public string? TransactionHash { get; set; }
    public BankResponse Bank { get; set; } = new();
    public long? AuthorisedDate { get; set; }
    public long CapturedDate { get; set; }
    public long Amount { get; set; }
    public string? CurrencyCode { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? LastCardDigits { get; set; }
    public string? IsoFamilyCode { get; set; }
    public string? ProprietaryCode { get; set; }
    public string? Reference { get; set; }
    public string? CustomerEmailAddress { get; set; }
    public Address? LocationAddress { get; set; }
    public MerchantResponse Merchant { get; set; } = new();
    public MerchantTransactionTypes? MerchantTransactionType { get; set; }
}

public class BankResponse
{
    public string Id { get; set; } = string.Empty;
    public string TradingName { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
}

public enum MerchantTransactionTypes
{
    Anonymous,
    Customer,
    Supplier
}

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class BankTransactionsResponse : ISuccess
{
    public BankTransactionResponse BankTransactions { get; set; } = new();
}
