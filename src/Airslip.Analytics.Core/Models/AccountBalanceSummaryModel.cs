using Airslip.Analytics.Core.Enums;
using System;

namespace Airslip.Analytics.Core.Models;

public record AccountBalanceSummaryModel(string Id,
    string InstitutionId,
    AccountStatus AccountStatus,
    string? SortCode,
    string? AccountNumber,
    string currencyCode,
    double Balance,
    DateTime UpdatedOn);