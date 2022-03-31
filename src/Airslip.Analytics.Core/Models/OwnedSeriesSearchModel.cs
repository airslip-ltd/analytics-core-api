namespace Airslip.Analytics.Core.Models;

public record OwnedSeriesSearchModel : OwnedSearchModel
{
    public string StartDate { get; init; }
    public string EndDate { get; init; }
    public string? IntegrationId { get; init; }
}