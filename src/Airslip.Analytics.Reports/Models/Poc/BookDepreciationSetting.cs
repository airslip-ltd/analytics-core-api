namespace Airslip.Analytics.Reports.Models.Poc;

public class BookDepreciationSetting
{
    /// <summary>
    /// The depreciation method
    /// </summary>
    public DepreciationMethods DepreciationMethod { get; set; }
    /// <summary>
    /// The averaging method
    /// </summary>
    public AveragingMethods AveragingMethod { get; set; }
    /// <summary>
    /// The depreciation rate
    /// </summary>
    public long DepreciationRate { get; set; }
    /// <summary>
    /// The depreciation calculation method
    /// </summary>
    public DepreciationCalculationMethods DepreciationCalculationMethod { get; set; }
}