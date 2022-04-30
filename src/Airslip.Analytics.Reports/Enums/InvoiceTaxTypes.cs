namespace Airslip.Analytics.Reports.Enums;

public enum InvoiceTaxTypes
{
    /// <summary>
    /// Line items are exclusive of tax
    /// </summary>
    Exclusive,
    /// <summary>
    /// Line items are inclusive tax
    /// </summary>
    Inclusive,
    /// <summary>
    /// Line have no tax
    /// </summary>
    NoTax
}