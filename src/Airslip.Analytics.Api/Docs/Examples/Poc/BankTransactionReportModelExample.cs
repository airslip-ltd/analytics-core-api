using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Core.Poc;
using Airslip.Analytics.Reports.Models;
using Airslip.Common.Types.Enums;
using Airslip.Common.Utilities;
using Airslip.Integrations.Banking.Types.Enums;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

namespace Airslip.Analytics.Api.Docs.Examples.Poc;

public class BankTransactionReportModelExample : IExamplesProvider<BankTransactionReportModel>
{
    public BankTransactionReportModel GetExamples()
    {
        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        long oneDayMilliseconds = 86400000;
        return new BankTransactionReportModel
        {
            Id = "4873a037963341e0b5f9de6b8260b8b2",
            BankTransactionId = CommonFunctions.GetId(),
            TransactionHash = "adad72d7b3069ab9e4a6cb2844e2e3e9.1",
            Bank = new IntegrationModel
            {
                Id = CommonFunctions.GetId(),
                IntegrationProviderId = "amex",
                IntegrationProviderFriendlyName = "American Express",
                Name = "American Express Business Current Account",
                AccountDetail = new IntegrationAccountDetailModel
                {
                    Id = CommonFunctions.GetId(),
                    AccountStatus = BankingAccountStatus.Active,
                    AccountId = CommonFunctions.GetId(),
                    LastCardDigits = "1234",
                    CurrencyCode = Iso4217CurrencyCodes.GBP.ToString(),
                    UsageType = BankingUsageTypes.BUSINESS,
                    AccountType = BankingAccountTypes.CURRENT,
                    SortCode = "01-23-45",
                    AccountNumber = "12345678"
                }
            },
            AuthorisedDate = timestamp - oneDayMilliseconds,
            CapturedDate = timestamp,
            Amount = 359900,
            CurrencyCode = Iso4217CurrencyCodes.GBP.ToString(),
            Description = "SLACK T01GDPJA77D       DUBLIN",
            LastCardDigits = "6578",
            IsoFamilyCode = "ICDT",
            ProprietaryCode = "Debit",
            Reference = "T01GDPJA77D",
            Merchant = new MerchantResponse
            {
                Id = CommonFunctions.GetId(),
                LegalName = "Slack Technologies Limited",
                TradingName = "Slack",
                CompanyNumber = "IE558379",
                TaxNumber = "GB369212978",
                Category = new Categorisation
                {
                    Name = Categories.ComputerSoftware,
                    Iso18245MerchantCategoryCode = "7372"
                },
                MerchantIdentifierNumber = null,
                Subsidiaries = new[]
                {
                    "Lattice", "Awesome and Howdy", "Small Wins", "Rimeto", "Spaces, Inc", "Screenhero, Inc",
                    "Woven Software, Inc"
                },
                ParentCompany = "Salesforce",
                HeadquartersAddress = new Address
                {
                    FirstLine = "One Park Place, 4th Floor",
                    SecondLine = "Hatch Street Dublin 2",
                    Locality = "Saint Kevin's",
                    AdministrativeArea = "Dublin",
                    SubAdministrativeArea = null,
                    PostalCode = "D02 E651",
                    CountryCode = Alpha2CountryCodes.IL
                },
                ContactDetail = new ContactDetail
                {
                    ContactNumber = "01234567890",
                    Email = "example@slack.com",
                    WebsiteUrl = "https://slack.com"
                },
                Directors = new List<DirectorDetail>
                {
                    new()
                    {
                        FirstName = "Stewart",
                        Surname = "Butterfield",
                        Nationality = Alpha2CountryCodes.CA.ToString(),
                        CountryOfResidence = Alpha2CountryCodes.US.ToString(),
                        HasNegativeInfo = false,
                        SigningAuthority = true,
                        Address = null,
                        Position = new DirectorPosition
                        {
                            Name = "Chief Executive Officer",
                            Authority = "",
                            DateAppointedTimestamp = 1645967251000,
                            CompanyName = "Slack Technologies Limited",
                            CompanyNumber = "IE558379",
                            LatestTurnoverFigure = 63000000000,
                            LatestTurnoverCurrency = Iso4217CurrencyCodes.USD,
                            Status = JobStatus.Current,
                            CommonCode = null,
                            ProviderCode = "LLC",
                            CreditScore = "999",
                            LatestRatingChangeTimestamp = timestamp,
                            AdditionalData = { },
                        },
                    }
                }
            },
            MerchantTransactionType = MerchantTransactionTypes.Supplier
        };
    }
}