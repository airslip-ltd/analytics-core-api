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

public class RelationshipService : IRelationshipService
{
    private readonly IRepository<RelationshipHeader, RelationshipHeaderModel> _repository;

    public RelationshipService(IRepository<RelationshipHeader, RelationshipHeaderModel> repository)
    {
        _repository = repository;
    }
    
    public async Task Execute(string message, DataSources dataSource)
    {
        // Turn to object
        RawPartnerRelationshipModel relationshipModel = Json.Deserialize<RawPartnerRelationshipModel>(message);

        if (relationshipModel.Id == null || relationshipModel.RelationshipStatus == RelationshipStatus.Invited) return;

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
            EntityId = relationshipModel.EntityId,
            DataSource = dataSource,
            EntityStatus = EntityStatus.Active,
            TimeStamp = relationshipModel.TimeStamp,
            UserId = relationshipModel.UserId,
            AirslipUserType = relationshipModel.AirslipUserType
        };

        List<string> approvedAccess = relationshipModel
            .Permission
            // .Where(o => o.ApprovedByUserId is not null)
            .Select(o => o.PermissionType)
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
                OwnerEntityId = relationshipModel.Related.EntityId!,
                OwnerAirslipUserType = relationshipModel.Related.AirslipUserType,
                ViewerEntityId = relationshipModel.EntityId!,
                ViewerAirslipUserType = relationshipModel.AirslipUserType
            });
        }

        // Update access rights
        foreach (RelationshipDetail item in model.Details)
        {
            item.Allowed = relationshipModel.RelationshipStatus == RelationshipStatus.Approved && 
                           approvedAccess.Contains(item.PermissionType);
        }

        await _repository.Upsert(model.Id!, model, model.UserId);
        
        if (relationshipModel.EntityStatus == EntityStatus.Deleted)
            await _repository.Delete(model.Id!, model.UserId);
    }
}