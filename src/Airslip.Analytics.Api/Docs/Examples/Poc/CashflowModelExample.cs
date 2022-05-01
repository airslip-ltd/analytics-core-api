using Airslip.Analytics.Reports.Enums;
using Airslip.Analytics.Reports.Models.Poc;
using Airslip.Common.Repository.Types.Enums;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Airslip.Analytics.Api.Docs.Examples.Poc;

// Need to complete
public class CashflowModelExample : IExamplesProvider<CashflowModel>
{
    public CashflowModel GetExamples()
    {
        return new CashflowModel
        {
            StartDate = 0,
            EndDate = 0,
            CashBalance = new CashBalance
            {
                OpeningCashBalance = 0,
                ClosingCashBalance = 0,
                NetCashMovement = 0
            },
            CashflowActivities = new List<CashflowActivity>()
            {
                new()
                {
                    Name = "",
                    Total = 0,
                    CashflowTypes = new List<CashflowType>()
                    {
                        new()
                        {
                            Name = "",
                            Total = 0,
                            Accounts = new List<CashflowType.CashflowAccount>()
                            {
                                new()
                                {
                                    Id = "",
                                    Type = AccountingAccountTypes.Cash,
                                    ClassType = AccountingAccountClassTypes.Asset,
                                    Code = "",
                                    Name = "",
                                    ReportingCode = "",
                                    Total = 0
                                }
                            }
                        }
                    }
                }
            },
        };
    }
}