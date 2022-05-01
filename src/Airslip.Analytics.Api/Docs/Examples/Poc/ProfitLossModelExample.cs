using Airslip.Analytics.Reports.Enums;
using Airslip.Analytics.Reports.Models.Poc;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Airslip.Analytics.Api.Docs.Examples.Poc;

// Need to complete
public class ProfitLossModelExample : IExamplesProvider<ProfitLossModel>
{
    public ProfitLossModel GetExamples()
    {
        return new ProfitLossModel
        {
            StartDate = ExampleValues.Dates.OneYearAgo,
            EndDate = ExampleValues.Dates.Today,
            NetProfitLoss = 0,
            Reports = new List<ProfitLossReport>
            {
                new()
                {
                    ProfitLossType = ProfitLossTypes.Revenue,
                    AccountTypes = new List<AccountType<ProfitLossAccount>>
                    {
                        new()
                        {
                            Type = AccountingAccountTypes.OtherIncome,
                            Accounts = new List<ProfitLossAccount>
                            {
                                new()
                                {
                                    Code = "",
                                    Id = "",
                                    Name = "null",
                                    ReportingCode = "",
                                    Total = 0
                                }
                            },
                            Total = 0
                        }
                    },
                    Total = 0
                },
                new()
                {
                    ProfitLossType = ProfitLossTypes.Expense,
                    AccountTypes = new List<AccountType<ProfitLossAccount>>
                    {
                        new()
                        {
                            Type = AccountingAccountTypes.DirectCosts,
                            Accounts = new List<ProfitLossAccount>
                            {
                                new()
                                {
                                    Code = "",
                                    Id = "",
                                    Name = "null",
                                    ReportingCode = "",
                                    Total = 0,
                                }
                            },
                            Total = 0
                        }
                    },
                    Total = 0
                }
            },
        };
    }
}