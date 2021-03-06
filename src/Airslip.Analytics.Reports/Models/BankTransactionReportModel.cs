using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Core.Poc;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Interfaces;
using Newtonsoft.Json;

namespace Airslip.Analytics.Reports.Models;

public class BankTransactionReportModel : IModel, ISuccess
{
    public static EntitySearchSortModel DefaultSort = new(
        nameof(CapturedDate), SortOrder.Desc); 

    public string? Id { get; set; }
    public string BankTransactionId { get; set; } = string.Empty;
    public string? TransactionHash { get; set; }
    public IntegrationReportModel Integration { get; set; } = new();
    public long? AuthorisedDate { get; set; }
    public long CapturedDate { get; set; }
    public long Amount { get; set; }
    public string? CurrencyCode { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? LastCardDigits { get; set; }
    public string? IsoFamilyCode { get; set; }
    public string? ProprietaryCode { get; set; }
    public string? Reference { get; set; }
    public MerchantResponse Merchant { get; set; } = new();
    public BusinessTypes? MerchantTransactionType { get; set; }
    public EntityStatus EntityStatus { get; set; } // This needs to be ignored for API docs
}