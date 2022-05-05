using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Reports.Models.Poc;

public class AssetModel : IModel, ISuccess
{
    /// <summary>
    /// The airslip identifier for the asset
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The name of the asset
    /// </summary>
    public string AssetName { get; set; } = string.Empty;
    /// <summary>
    /// The unique asset number
    /// </summary>
    public string AssetNumber { get; set; }  = string.Empty;
    /// <summary>
    /// The date the asset was purchased
    /// </summary>
    public long PurchaseDate { get; set; }
    /// <summary>
    /// The purchase price of the asset
    /// </summary>
    public long PurchasePrice { get; set; }
    /// <summary>
    /// The price the asset was disposed at
    /// </summary>
    public long? DisposalPrice { get; set; }
    /// <summary>
    /// The asset status
    /// </summary>
    public AssetStatus AssetStatus { get; set; }
    /// <summary>
    /// The accounting value of the asset
    /// </summary>
    public long AccountingBookValue { get; set; }
    /// <summary>
    /// The depreciation settings
    /// </summary>
    public BookDepreciationSetting BookDepreciationSetting { get; set; } = new();
    /// <summary>
    /// Details of the depreciation
    /// </summary>
    public BookDepreciationDetail BookDepreciationDetail { get; set; }= new();

    public EntityStatus EntityStatus { get; set; } // Ignore
}

public enum AssetStatus
{
    Draft,
    Registered,
    Disposed
}

public enum AveragingMethods
{
    /// <summary>
    /// If the asset depreciation method is by days
    /// </summary>
    ActualDays,
    /// <summary>
    /// If the asset depreciation method is by the full month 
    /// </summary>
    FullMonth
}

public enum DepreciationCalculationMethods
{
    /// <summary>
    /// Depreciation Rate (e.g. 20%)
    /// </summary>
    Rate,
    /// <summary>
    /// Effective life (e.g. 5 years)
    /// </summary>
    Life,
    /// <summary>
    /// None
    /// </summary>
    None
}

public enum DepreciationMethods
{
    /// <summary>
    /// No Depreciation
    /// </summary>
    NoDepreciation,
    /// <summary>
    /// Straight Line
    /// </summary>
    StraightLine,
    /// <summary>
    /// Diminishing Value at 100%
    /// </summary>
    DiminishingValue100,
    /// <summary>
    /// Diminishing Value at 150%
    /// </summary>
    DiminishingValue150,
    /// <summary>
    /// Diminishing Value at 200%
    /// </summary>
    DiminishingValue200,
    /// <summary>
    /// Full Depreciation at purchase
    /// </summary>
    FullDepreciation
}

public class BookDepreciationDetail
{
    /// <summary>
    /// When an asset is disposed, this will be the sell price minus the purchase price if a profit was made.
    /// </summary>
    public long? CurrentCapitalGain { get; set; }
    /// <summary>
    /// When an asset is disposed, this will be the lowest one of sell price or purchase price, minus the current book value.
    /// </summary>
    public long? CurrentGainLoss { get; set; }
    /// <summary>
    /// The depreciation start date
    /// </summary>
    public long DepreciationStartDate { get; set; }
    /// <summary>
    /// The value of the asset you want to depreciate, if this is less than the cost of the asset.
    /// </summary>
    public long CostLimit { get; set; }
    /// <summary>
    /// The value of the asset remaining when you've fully depreciated it.
    /// </summary>
    public long ResidualValue { get; set; }
    /// <summary>
    /// All depreciation prior to the current financial year.
    /// </summary>
    public long PriorAccumulativeDepreciationAmount { get; set; }
    /// <summary>
    /// All depreciation occurring in the current financial year.
    /// </summary>
    public long CurrentAccumulativeDepreciationAmount { get; set; }
}