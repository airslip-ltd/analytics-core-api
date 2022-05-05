using Airslip.Analytics.Reports.Models.Poc;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types;
using Airslip.Common.Types.Enums;
using Airslip.Common.Utilities;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

namespace Airslip.Analytics.Api.Docs.Examples.Poc;

public class EmployeeSearchModelExample : IExamplesProvider<EntitySearchResponse<EmployeeModel>>
{
    public EntitySearchResponse<EmployeeModel> GetExamples()
    {
        return new EntitySearchResponse<EmployeeModel>
        {
            Paging = new EntitySearchPagingModel
            {
                TotalRecords = 1,
                RecordsPerPage = 10,
                Page = 1,
                Next = null
            },
            Results = new List<EmployeeModel>
            {
                new()
                {
                    Id = CommonFunctions.GetId(),
                    Title = "Mr",
                    FirstName = "Derek",
                    LastName = "Longer",
                    DateOfBirthUtc = DateTime.Parse("1968-10-20"),
                    Gender = Genders.Male,
                    Email = "dlonger@airslip.com",
                    PhoneNumber = "07123456789",
                    StartDate = ExampleValues.Dates.ThirtyDaysAgo,
                    NationalInsuranceNumber = "JL329791A",
                    IsOffPayrollWorker = false,
                    Address = new Address
                    {
                        FirstLine = "One Park Place, 4th Floor",
                        SecondLine = "Hatch Street Dublin 2",
                        Locality = "Saint Kevin's",
                        AdministrativeArea = "Dublin",
                        SubAdministrativeArea = null,
                        PostalCode = "D02 E651",
                        CountryCode = Alpha2CountryCodes.IL
                    },
                    PayrollCalendarId = CommonFunctions.GetId(),
                    UpdatedDate = ExampleValues.Dates.Today,
                    CreatedDate = ExampleValues.Dates.ThirtyDaysAgo,
                    NiCategories = new List<NiCategory>
                    {
                        new()
                        {
                            Category = "A",
                            StartDate = ExampleValues.Dates.ThirtyDaysAgo
                        }
                    },
                    EmployeeNumber = "A12345678",
                    EndDate = null
                }
            }
        };
    }
}