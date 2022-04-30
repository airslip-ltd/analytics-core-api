namespace Airslip.Analytics.Reports.Enums;

public enum InvoiceStatus
{
    /// <summary>
    /// A Draft Invoice ( default)
    /// </summary>
    Draft,
    /// <summary>
    /// Invoice is no longer a draft. It has been processed and, or, sent to the customer. In this state, it will impact the ledger. It also has no payments made against it
    /// </summary>
    Submitted,
    /// <summary>
    /// A Deleted Invoice
    /// </summary>
    Deleted,
    /// <summary>
    /// An Invoice that is Approved and Awaiting Payment OR partially paid
    /// </summary>
    Authorised,
    /// <summary>
    /// The balance paid against the invoice is positive, but less than the total invoice amount
    /// </summary>
    PartiallyPaid,
    /// <summary>
    /// An Invoice that is completely Paid
    /// </summary>
    Paid,
    /// <summary>
    /// An invoice can become Void when it's deleted, refunded, written off, or cancelled. A voided invoice may still be `PartiallyPaid`, and so all outstanding amounts on voided invoices are removed from the accounts receivable account
    /// </summary>
    Voided	
}