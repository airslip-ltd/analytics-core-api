using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Core.Extensions;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Reports.Data;
using Airslip.Analytics.Reports.Interfaces;
using Airslip.Analytics.Reports.Models;
using Airslip.Analytics.Services.SqlServer;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Reports.Implementations;

public class CommerceProviderReport : ICommerceProviderReport
{
    private readonly IEntitySearch<CommerceProviderModel> _entitySearch;
    private readonly SqlServerContext _context;
    private readonly UserToken _userToken;

    public CommerceProviderReport(IContext context, IEntitySearch<CommerceProviderModel> entitySearch,
        ITokenDecodeService<UserToken> tokenDecodeService)
    {
        if (context is not SqlServerContext dbContext) 
            throw new ArgumentException("Invalid context", nameof(context));
        _entitySearch = entitySearch;
        _context = dbContext;
        _userToken = tokenDecodeService.GetCurrentToken();
    }

    public async Task<IResponse> Execute(OwnedDataSearchModel query)
    {
        IQueryable<CommerceProviderReportQuery> qBalance = 
            from rd in _context.RelationshipDetails
            from integration in _context.Integrations.Where(o => o.EntityId.Equals(rd.OwnerEntityId) && o.AirslipUserType == rd.OwnerAirslipUserType)
            from provider in _context.IntegrationProviders.Where(o => o.Id.Equals(integration.IntegrationProviderId))
            where integration.IntegrationType == IntegrationType.Commerce
            select new CommerceProviderReportQuery
            {
                Id = integration.Id,
                AuditInformation = integration.AuditInformation,
                EntityStatus = integration.EntityStatus,
                IntegrationProviderId = integration.IntegrationProviderId,
                Name = integration.Name,
                AuthenticationState = integration.AuthenticationState,
                TimeStamp = integration.TimeStamp,
                Provider = provider,
                
                OwnerEntityId = rd.OwnerEntityId,
                OwnerAirslipUserType = rd.OwnerAirslipUserType,
                ViewerEntityId = rd.ViewerEntityId,
                ViewerAirslipUserType = rd.ViewerAirslipUserType,
                PermissionType = rd.PermissionType,
                Allowed = rd.Allowed
            };

        EntitySearchResponse<CommerceProviderModel> searchResults = await _entitySearch
            .GetSearchResults(qBalance, query, 
                new List<SearchFilterModel>
                {
                    new(nameof(CommerceProviderReportQuery.OwnerEntityId), query.OwnerEntityId),
                    new(nameof(CommerceProviderReportQuery.OwnerAirslipUserType), query.OwnerAirslipUserType.ToString()),
                    new(nameof(CommerceProviderReportQuery.ViewerEntityId), _userToken.EntityId),
                    new(nameof(CommerceProviderReportQuery.ViewerAirslipUserType), _userToken.AirslipUserType.ToString()),
                    new(nameof(CommerceProviderReportQuery.PermissionType), PermissionType.Banking.ToString()),
                    new(nameof(CommerceProviderReportQuery.Allowed), true)
                });
        
        return searchResults;
    }

    public Task<IResponse> Download(OwnedDataSearchModel query)
    {
        return Execute(query);
    }
}