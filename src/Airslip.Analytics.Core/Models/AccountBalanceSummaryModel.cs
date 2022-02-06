using Airslip.Analytics.Core.Enums;
using System;

namespace Airslip.Analytics.Core.Models;

public record AccountBalanceSummaryModel(string InstitutionId,
    AccountStatus AccountStatus,
    string? SortCode,
    string? AccountNumber,
    double Balance,
    DateTime UpdatedOn
);