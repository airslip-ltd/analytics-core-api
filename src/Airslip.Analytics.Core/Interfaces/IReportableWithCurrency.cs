namespace Airslip.Analytics.Core.Interfaces;

public interface IReportableWithCurrency : IReportableWithOwnership
{
    string CurrencyCode { get; init; }
}