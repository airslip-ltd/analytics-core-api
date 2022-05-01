using Airslip.Analytics.Reports.Enums;
using Airslip.Analytics.Reports.Models.Poc;
using Airslip.Common.Utilities;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Airslip.Analytics.Api.Docs.Examples.Poc;

public class BalanceSheetModelExample : IExamplesProvider<BalanceSheetModel>
{
    public BalanceSheetModel GetExamples()
    {
        return new BalanceSheetModel
        {
            Id = "12b84d2970984e309e8bf4dc9af7812f",
            BalanceDate = 0,
            Reports = new List<BalanceSheetReport>
            {
                new()
                {
                    BalanceSheetType = BalanceSheetTypes.Asset,
                    AccountTypes = new List<AccountType>
                    {
                        new()
                        {
                            Type = AccountingAccountTypes.Inventory,
                            Accounts = new List<AccountType.Account>
                            {
                                new()
                                {
                                    Code = "630",
                                    Id = CommonFunctions.GetId(),
                                    Name = "Inventory",
                                    ReportingCode = "ASS.CUR.INY",
                                    Total = 12289
                                }
                            },
                            Total = 12289
                        }
                    },
                    Total = 12289
                },
                new()
                {
                    BalanceSheetType = BalanceSheetTypes.Liability,
                    AccountTypes = new List<AccountType>
                    {
                        new()
                        {
                            Type = AccountingAccountTypes.CurrentLiability,
                            Accounts = new List<AccountType.Account>
                            {
                                new()
                                {
                                    Code = "610",
                                    Id = CommonFunctions.GetId(),
                                    Name = "GST",
                                    ReportingCode = "LIA.CUR.TAX.GST",
                                    Total = 1239
                                }
                            },
                            Total = 1239
                        }
                    },
                    Total = 1239
                },
                new()
                {
                    BalanceSheetType = BalanceSheetTypes.Equity,
                    AccountTypes = new List<AccountType>
                    {
                        new()
                        {
                            Type = AccountingAccountTypes.Equity,
                            Accounts = new List<AccountType.Account>
                            {
                                new()
                                {
                                    Code = "122",
                                    Id = CommonFunctions.GetId(),
                                    Name = "Current Year Earnings",
                                    ReportingCode = "EQU.CUR.YR",
                                    Total = 1418
                                }
                            },
                            Total = 1418
                        }
                    },
                    Total = 1418
                }
            }
        };
    }
}