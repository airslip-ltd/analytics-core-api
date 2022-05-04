using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Reports.Models.Poc;

public class AssetTypeModel : IModel, ISuccess
{
    /// <summary>
    /// The name of the asset type
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// The asset account for fixed assets of this type
    /// </summary>
    public string FixedAssetAccountId { get; set; } = string.Empty;
    /// <summary>
    /// The expense account for the depreciation of fixed assets of this type
    /// </summary>
    public string DepreciationExpenseAccountId { get; set; } = string.Empty;
    /// <summary>
    /// The account for accumulated depreciation of fixed assets of this type
    /// </summary>
    public string AccumulatedDepreciationAccountId { get; set; } = string.Empty;
    /// <summary>
    /// The depreciation settings
    /// </summary>
    public BookDepreciationSetting BookDepreciationSetting { get; set; } = new();
    
    public EntityStatus EntityStatus { get; set; } // Ignore
    public string? Id { get; set; }
}