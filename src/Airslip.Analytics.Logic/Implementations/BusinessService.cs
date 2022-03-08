using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Core.Models.Raw.CustomerPortal;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Enums;
using Airslip.Common.Utilities;

namespace Airslip.Analytics.Logic.Implementations;

public class BusinessService : IBusinessService
{
    private readonly IRepository<RelationshipHeader, RelationshipHeaderModel> _repository;

    public BusinessService(IRepository<RelationshipHeader, RelationshipHeaderModel> repository)
    {
        _repository = repository;
    }
    
    public async Task Execute(string message, DataSources dataSource)
    {
        // Turn to object
        RawBusinessModel relationshipModel = Json.Deserialize<RawBusinessModel>(message);

        if (relationshipModel.Id == null) return;

        // Look in the repo for an existing model
        RepositoryActionResultModel<RelationshipHeaderModel> getResult = await _repository.Get(relationshipModel.Id);

        RelationshipHeaderModel? model = null;
        
        if (getResult is SuccessfulActionResultModel<RelationshipHeaderModel> success)
        {
            model = success.CurrentVersion;
        }

        model ??= new RelationshipHeaderModel
        {
            Id = relationshipModel.Id,
            EntityId = relationshipModel.Id,
            DataSource = dataSource,
            EntityStatus = relationshipModel.EntityStatus,
            TimeStamp = relationshipModel.TimeStamp,
            UserId = relationshipModel.PrimaryUserId,
            AirslipUserType = AirslipUserType.Merchant
        };

        List<string> approvedAccess = Enum
            .GetValues(typeof(PermissionType))
            .Cast<PermissionType>()
            .Select(o => o.ToString())
            .ToList();
        
        List<string> currentItems = model
            .Details
            .Select(o => o.PermissionType)
            .ToList();
        
        // Add missing items
        foreach (string permissionType in approvedAccess.Where(o => !currentItems.Contains(o)))
        {
            model.Details.Add(new RelationshipDetail
            {
                Id = CommonFunctions.GetId(),
                PermissionType = permissionType,
                OwnerEntityId = relationshipModel.Id,
                OwnerAirslipUserType = AirslipUserType.Merchant,
                ViewerEntityId = relationshipModel.Id,
                ViewerAirslipUserType = AirslipUserType.Merchant
            });
        }

        // Update access rights
        foreach (RelationshipDetail item in model.Details)
        {
            item.Allowed = relationshipModel.EntityStatus == EntityStatus.Active;
        }

        await _repository.Upsert(model.Id!, model, model.UserId);
    }
}