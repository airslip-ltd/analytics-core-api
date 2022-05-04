using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Reports.Models.Poc;

public class EmployeeModel : IModel, ISuccess
{
    /// <summary>
    /// The airslip identifier for the employee
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Title of the employee
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// First name of the employee
    /// </summary>
    public string FirstName { get; set; }= string.Empty;
    
    /// <summary>
    /// Last name of the employee
    /// </summary>
    public string LastName { get; set; }= string.Empty;
    
    /// <summary>
    /// Date of birth of the employee in UTC
    /// </summary>
    public DateTime DateOfBirthUtc { get; set; }
    /// <summary>
    /// The employee’s gender
    /// </summary>
    public Genders Gender { get; set; }
    /// <summary>
    /// E-mail address of the employee
    /// </summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// Phone number of the employee
    /// </summary>
    public string PhoneNumber { get; set; }= string.Empty;
    /// <summary>
    /// Employment start date of the employee at the time it was requested
    /// </summary>
    public long StartDate { get; set; }
    /// <summary>
    /// National insurance number of the employee
    /// </summary>
    public string NationalInsuranceNumber { get; set; }= string.Empty;
    /// <summary>
    /// Flag for whether the employee is an off-payroll worker
    /// </summary>
    public bool IsOffPayrollWorker { get; set; }
    /// <summary>
    /// Employee home address
    /// </summary>
    public Address Address { get; set; } = new();
    /// <summary>
    /// Payroll calendar identifier of the employee
    /// </summary>
    public string PayrollCalendarId { get; set; }= string.Empty;
    /// <summary>
    /// Last update to the employee
    /// </summary>
    public long UpdatedDate { get; set; }
    /// <summary>
    /// When the employee was created
    /// </summary>
    public long CreatedDate { get; set; }
    /// <summary>
    /// The NI Category information
    /// </summary>
    public List<NiCategory> NiCategories { get; set; } = new();
    /// <summary>
    /// The employment number of the employee
    /// </summary>
    public string EmployeeNumber { get; set; }= string.Empty;
    /// <summary>
    /// Employment end date of the employee
    /// </summary>
    public long? EndDate { get; set; }

    public EntityStatus EntityStatus { get; set; }
}

public enum Genders
{
    Female,
    Male,
    Other
}

public class NiCategory
{
    /// <summary>
    /// Start date of the NI Category
    /// </summary>
    public long StartDate { get; set; }
    /// <summary>
    /// The National Insurance category code of the employee
    /// </summary>
    public string Category { get; set; }= string.Empty;
}
