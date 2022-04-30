using Airslip.Analytics.Core.Poc;
using Airslip.Analytics.Reports.Enums;
using Airslip.Analytics.Reports.Models.Poc;
using Airslip.Common.Types.Enums;
using Airslip.Common.Utilities;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Airslip.Analytics.Api.Docs.Examples.Poc;

public class InvoiceModelExample : IExamplesProvider<InvoiceModel>
{
    public InvoiceModel GetExamples()
    {
        return new InvoiceModel
        {
            Id = "66b84d2970984e359e8bf4dc9af7812e",
            Type = InvoiceTypes.AccountReceivable,
            Contact = new Contact
            {
                Id = CommonFunctions.GetId(),
                ContactStatus = ContactStatus.Active,
                CompanyName = "Microsoft",
                FirstName = "Derek",
                Surname = "Longer",
                Email = "dlonger@company.com",
                PhoneNumber = "07987123456",
                Addresses = null,
                UpdatedDate = ExampleValues.Dates.Now,
                Type = BusinessTypes.Supplier
            },
            IssuedDate = ExampleValues.Dates.ThirtyDaysAgo,
            DueDate = ExampleValues.Dates.Today,
            Status = InvoiceStatus.Paid,
            InvoiceTaxType = InvoiceTaxTypes.Exclusive,
            LineItems = new List<LineItem>
            {
                new()
                {
                    Id = CommonFunctions.GetId(),
                    ItemCode = "A123",
                    AccountCode = "AIRSLIP_A123",
                    Description = "Microsoft Azure Services",
                    Quantity = 1,
                    UnitAmount = 140730,
                    TaxDetails = new List<TaxDetail>
                    {
                        new()
                        {
                            TaxRateReference = "20",
                            Amount = 28145,
                        }
                    },
                    DiscountDetails = null,
                    LineAmount = 168875,
                    TrackingCategories = new List<TrackingCategory>
                    {
                        new()
                        {
                            Id = CommonFunctions.GetId(),
                            Name = "IT Infrastructure",
                            CountryCode = Alpha2CountryCodes.GB
                        }
                    }
                }
            },
            SubTotal = 140730,
            TotalTax = 28145,
            Total = 168875,
            CreatedDate = ExampleValues.Dates.Today,
            UpdatedDate = ExampleValues.Dates.Today,
            CurrencyCode = Iso4217CurrencyCodes.GBP,
            InvoiceNumber = "E0700GKMNI",
            BankTransactionId = "5120a037963341e0b5f9de6b8260b7b9",
            BusinessId = "ca56467b498c48f3adf3c160ae3e0e56",
            Reference = null,
            ExpectedPaymentDate = ExampleValues.Dates.Today,
            PlannedPaymentDate = ExampleValues.Dates.Today,
            RepeatingInvoiceID = null,
            CISDeduction = null,
            Payments = new List<Payment>
            {
                new()
                {
                    Id = CommonFunctions.GetId(),
                    PaymentType = PaymentTypes.StandardPayment,
                    Date = ExampleValues.Dates.Today,
                    Amount = 168875,
                }
            },
            CreditNotes = null,
            AmountDue = 0,
            AmountPaid = 168875,
            FullyPaidOnDate = ExampleValues.Dates.Today,
            AmountCredited = 0
        };
    }
}