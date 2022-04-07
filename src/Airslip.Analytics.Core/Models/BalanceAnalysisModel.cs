using Airslip.Common.Types.Enums;

namespace Airslip.Analytics.Core.Models;

public record BalanceAnalysisModel(string EntityId, AirslipUserType AirslipUserType) 
    : EntityAnalysisModel(EntityId, AirslipUserType);