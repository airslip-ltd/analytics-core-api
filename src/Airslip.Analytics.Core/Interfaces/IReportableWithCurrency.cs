namespace Airslip.Analytics.Core.Interfaces;

public interface IReportableWithCurrency : IReportableWithOwnership
{
    string? Currency { get; init; }
}