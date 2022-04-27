using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Poc;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class MerchantResponse : ISuccess
{
    public string? Id { get; set; }
    public string? LegalName { get; set; }
    public string? TradingName { get; set; }
    public string? CompanyNumber { get; set; }
    public string? TaxNumber { get; set; }
    public Categorisation? Category { get; set; } = new();
    public string? MerchantIdentifierNumber { get; set; }
    public string[]? Subsidiaries { get; set; }
    public string? ParentCompany { get; set; }
    public Address? HeadquartersAddress { get; set; } = new();
    public ContactDetail? ContactDetail { get; set; } = new();
    public IEnumerable<DirectorDetail>? Directors { get; set; } = new List<DirectorDetail>();
    public string? LogoUrl { get; set; }
}

public class DirectorDetail
{
    public string? FirstName { get; set; }
    public string? Surname { get; set; }
    public string? Nationality { get; set; }
    public string? CountryOfResidence { get; set; }
    public bool? HasNegativeInfo { get; set; }
    public bool? SigningAuthority { get; set; }
    public Address? Address { get; set; } = new();
    public DirectorPosition? Position { get; set; } = new();
}

public class DirectorPosition
{
    /// <summary>
    /// Name of Job Title
    /// </summary>
    public string? Name { get; set; }

    public string? Authority { get; set; }
    public long? DateAppointedTimestamp { get; set; }
    public string? CompanyName { get; set; }
    public string? CompanyNumber { get; set; }
    public long? LatestTurnoverFigure { get; set; }
    public Iso4217CurrencyCodes? LatestTurnoverCurrency { get; set; }
    public JobStatus? Status { get; set; }
    public string? CommonCode { get; set; }
    public string? ProviderCode { get; set; }
    public string? CreditScore { get; set; }
    public long? LatestRatingChangeTimestamp { get; set; }
    public object? AdditionalData { get; set; }
}

public enum JobStatus
{
    Current,
    Previous,
    Inactive
}

public enum Categories
{
    Advertising,
    BankFees,
    ComputerHardware,
    ComputerSoftware,
    ContractLabour,
    Design,
    EmployeePerks,
    Entertainment,
    Fuel,
    Groceries,
    Insurance,
    LegalServices,
    Mileage,
    OfficeFurniture,
    OfficeSupplies,
    PaymentProcessingFees,
    PostageAndShipping,
    ProfessionalServices,
    Rent,
    Salaries,
    ServerInfrastructure,
    Tax,
    Training,
    Travel,
    Utilities,
    Vehicle,
    VehicleMaintenance
}

public class Categorisation
{
    public Categories? Name { get; set; }
    public string? Iso18245MerchantCategoryCode { get; set; }
}

public class ContactDetail
{
    public string? Email { get; set; }
    public string? ContactNumber { get; set; }
    public string? WebsiteUrl { get; set; }
}

/// <summary>
/// Move to common
/// </summary>
public class Address
{
    /// <summary>
    /// First line of address
    /// </summary>
    public string? FirstLine { get; set; }

    /// <summary>
    /// Second line of address
    /// </summary>
    public string? SecondLine { get; set; }

    /// <summary>
    /// City / Town
    /// </summary>
    public string? Locality { get; set; }

    /// <summary>
    /// State / Province / Region
    /// </summary>
    public string? AdministrativeArea { get; set; }

    /// <summary>
    /// County / District
    /// </summary>
    public string? SubAdministrativeArea { get; set; }

    /// <summary>
    /// Postcode / ZIP Code
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// ALPHA-2 ISO 3166 country code <see cref="https://www.iso.org/iso-3166-country-codes.html" />
    /// </summary>
    public Airslip.Common.Types.Enums.Alpha2CountryCodes? CountryCode { get; set; }
}