using Airslip.Analytics.Core.Poc;
using Airslip.Analytics.Reports.Enums;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;
using Newtonsoft.Json;

namespace Airslip.Analytics.Reports.Models.Poc;

public class InvoiceModel : IModel, ISuccess
{
    /// <summary>
    /// Airslip identifier
    /// </summary>
    public string? Id { get; set; }
    
    /// <summary>
    /// Check whether this an account receivable (invoice) or account payable (bill)
    /// </summary>
    public InvoiceTypes Type { get; set; }

    /// <summary>
    /// Contact details for the invoice
    /// </summary>
    public Contact Contact { get; set; } = new();
    /// <summary>
    /// Date invoice was issued
    /// </summary>
    public long IssuedDate { get; set; }
    /// <summary>
    /// Date invoice is due
    /// </summary>
    public long DueDate { get; set; }
    /// <summary>
    /// The invoice status code
    /// </summary>
    public InvoiceStatus Status { get; set; }
    /// <summary>
    /// The tax type of the line items
    /// </summary>
    public InvoiceTaxTypes InvoiceTaxType { get; set; }
    /// <summary>
    /// Details of all the line items
    /// </summary>
    public List<LineItem> LineItems { get; set; } = new();
    /// <summary>
    /// Total of invoice excluding taxes
    /// </summary>
    public long SubTotal { get; set; }
    /// <summary>
    /// Total tax on invoice
    /// </summary>
    public long TotalTax { get; set; }
    /// <summary>
    /// Total of Invoice tax inclusive (i.e. SubTotal + TotalTax)
    /// </summary>
    public long Total { get; set; }
    /// <summary>
    /// Last modified date UTC format
    /// </summary>
    public long CreatedDate { get; set; }
    /// <summary>
    /// Last modified date UTC format
    /// </summary>
    public long UpdatedDate { get; set; }
    /// <summary>
    /// The currency that invoice has been raised in.
    /// </summary>
    public Iso4217CurrencyCodes CurrencyCode { get; set; }
    
    /// <summary>
    /// AccountReceivable - Unique alpha numeric code identifying invoice (printable ASCII characters only)
    /// AccountPayable - Non-unique alpha numeric code identifying invoice (printable ASCII characters only). This value will also display as Reference in the UI.
    /// </summary>
    public string InvoiceNumber { get; set; } = string.Empty;
    /// <summary>
    /// If a value is present then this has been reconciled against a bank transaction
    /// </summary>
    public string? BankTransactionId { get; set; }

    /// <summary>
    /// The id of the customer / supplier. Fetch the business from the business API
    /// </summary>
    public string? BusinessId { get; set; } = string.Empty;
    /// <summary>
    /// Account receivable only - additional reference number
    /// </summary>
    public string? Reference { get; set; } = string.Empty;
    
    /// <summary>
    /// Shown on sales invoices (Accounts Receivable) when this has been set
    /// </summary>
    public long ExpectedPaymentDate { get; set; }
    
    /// <summary>
    /// 	Shown on bills (Accounts Payable) when this has been set
    /// </summary>
    public long PlannedPaymentDate { get; set; }
    /// <summary>
    /// Unique identifier for repeating invoice template. Present only if the invoice is created as part of a Repeating Invoice
    /// </summary>
    public string? RepeatingInvoiceID { get; set; } = string.Empty;
    
    /// <summary>
    /// CISDeduction withheld by the contractor to be paid to HMRC on behalf of subcontractor (Available for organisations under UK Construction Industry Scheme)
    /// </summary>
    public string? CISDeduction { get; set; } = string.Empty;
    /// <summary>
    /// Details of all the payments
    /// </summary>
    public List<Payment> Payments { get; set; } = new();
    
    /// <summary>
    /// Details of credit notes that have been applied to an invoice
    /// </summary>
    public List<CreditNote>? CreditNotes { get; set; } = new();
    
    /// <summary>
    /// Amount remaining to be paid on invoice
    /// </summary>
    public long AmountDue { get; set; }
    
    /// <summary>
    /// Sum of payments received for invoice
    /// </summary>
    public long AmountPaid { get; set; }
    
    /// <summary>
    /// The date the invoice was fully paid. Only returned on fully paid invoices
    /// </summary>
    public long FullyPaidOnDate { get; set; }
    
    /// <summary>
    /// Sum of all credit notes, over-payments and pre-payments applied to invoice
    /// </summary>
    public long AmountCredited { get; set; }
    [JsonIgnore] public EntityStatus EntityStatus { get; set; }
}

public class Contact
{
    /// <summary>
    /// Airslip identifier
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The type of contact
    /// </summary>
    public BusinessTypes Type { get; set; }
    
    /// <summary>
    /// Current status of a contact
    /// </summary>
    public ContactStatus ContactStatus { get; set; }

    /// <summary>
    /// Full legal name of the business
    /// </summary>
    public string? CompanyName { get; set; } = string.Empty;
    
    /// <summary>
    /// First name of contact person
    /// </summary>
    public string? FirstName { get; set; } = string.Empty;
    
    /// <summary>
    /// First name of contact person
    /// </summary>
    public string? Surname { get; set; } = string.Empty;

    /// <summary>
    /// Email address of contact person
    /// </summary>
    public string? Email { get; set; } = string.Empty;

    /// <summary>
    /// Phone number of contact person
    /// </summary>
    public string? PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// All addresses of contact person 
    /// </summary>
    public List<Address>? Addresses { get; set; } = new();

    /// <summary>
    /// UTC timestamp of last update to contact
    /// </summary>
    public long UpdatedDate { get; set; }
}

public enum ContactStatus
{
    /// <summary>
    /// The contact is still active
    /// </summary>
    Active,
    /// <summary>
    /// An active contact has been archived 
    /// </summary>
    Archived,
    /// <summary>
    /// The contact has issued a GDPR request to remove their data from the system
    /// </summary>
    GdprRequest
}

public class TrackingCategory
{
    /// <summary>
    /// Airslip identifier
    /// </summary>
    public string Id { get; set; } = string.Empty;
    /// <summary>
    /// Name of the tracking category in the accounting platform
    /// </summary>
    public string Name { get; set; }= string.Empty;
    /// <summary>
    /// The location of the tracking category
    /// </summary>
    public Alpha2CountryCodes CountryCode { get; set; }
}

public class LineItem
{
    /// <summary>
    /// Airslip identifier
    /// </summary>
    public string Id { get; set; } = string.Empty;
    /// <summary>
    /// User defined item code
    /// </summary>
    public string ItemCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Customer defined alpha numeric account code
    /// </summary>
    public string AccountCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Description needs to be at least 1 char long. A line item with just a description (i.e no unit amount or quantity) can be created by specifying just a Description element that contains at least 1 character (max length = 4000)
    /// </summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Line item quantity
    /// </summary>
    public long Quantity { get; set; }
    /// <summary>
    /// Line item unit amount
    /// </summary>
    public long UnitAmount { get; set; }
    /// <summary>
    /// All tax details
    /// </summary>
    public List<TaxDetail> TaxDetails { get; set; } = new();
    
    /// <summary>
    /// The line amount reflects the discounted price if a DiscountRate has been used
    /// </summary>
    public long LineAmount { get; set; }
    
    /// <summary>
    /// Line item discounts, if there are any
    /// </summary>
    public List<DiscountDetail>? DiscountDetails { get; set; } = new();
    
    /// <summary>
    /// Tracking Categories lets you see how different areas of your business are performing, so you can make proactive business decisions.
    /// </summary>
    public List<TrackingCategory> TrackingCategories { get; set; } = new();
}

public class DiscountDetail
{
    /// <summary>
    /// The rate of the discount
    /// </summary>
    public string Rate { get; set; } = string.Empty;
    
    /// <summary>
    /// The amount of discount
    /// </summary>
    public long Amount { get; set; }
}

public class TaxDetail
{
    /// <summary>
    /// The description of the tax
    /// </summary>
    public string TaxRateReference { get; set; } = string.Empty;
    
    /// <summary>
    /// The amount of tax
    /// </summary>
    public long Amount { get; set; }
}

public class Payment
{
    /// <summary>
    /// Airslip identifier
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Determines whether the payment is a pre or over payment. Default is standard.
    /// </summary>
    public PaymentTypes PaymentType { get; set; }

    /// <summary>
    /// Date of the payment was captured 
    /// </summary>
    public long Date { get; set; }
    /// <summary>
    /// The payment amount
    /// </summary>
    public long Amount { get; set; }
}

public enum PaymentTypes
{
    StandardPayment,
    PrePayment,
    OverPayment
}