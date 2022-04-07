using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public abstract record EntityAnalysisModel(string EntityId, AirslipUserType AirslipUserType) : ITraceable
{
    public string TraceInfo => $"EntityId: {EntityId}, AirslipUserType: {AirslipUserType.ToString()}";
}