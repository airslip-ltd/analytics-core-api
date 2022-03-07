using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;

namespace Airslip.Analytics.Core.Interfaces;

public interface IReportableWithCurrency : IReportableWithOwnership
{
    string? Currency { get; init; }
}