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
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Reports.Implementations;

public class AccountBalanceReport : IAccountBalanceReport
{
    private readonly IEntitySearch<AccountBalanceReportModel> _entitySearch;
    private readonly SqlServerContext _context;
    private readonly UserToken _userToken;

    public AccountBalanceReport(IContext context, IEntitySearch<AccountBalanceReportModel> entitySearch,
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
        IQueryable<AccountBalanceReportQuery> qBalance = 
            from rd in _context.RelationshipDetails
            from integration in _context.Integrations.Where(o => o.EntityId.Equals(rd.OwnerEntityId) && o.AirslipUserType == rd.OwnerAirslipUserType)
            join bankAccountBalanceSummary in _context.BankAccountBalanceSummary on integration.Id
                equals bankAccountBalanceSummary.IntegrationId
            from accountDetail in _context.IntegrationAccountDetails.Where(o => o.IntegrationId.Equals(integration.Id))
            from countryCode in _context.CurrencyDetails.Where(o => o.Id.Equals(accountDetail.CurrencyCode))
            where integration.IntegrationType == IntegrationType.Banking
            select new AccountBalanceReportQuery
            {
                Id = integration.Id,
                IntegrationProviderId = integration.IntegrationProviderId,
                AccountStatus = accountDetail.AccountStatus,
                SortCode = accountDetail.SortCode,
                AccountNumber = accountDetail.AccountNumber,
                CurrencyCode = accountDetail.CurrencyCode,
                Balance = bankAccountBalanceSummary.Balance.ToCurrency(),
                UpdatedOn = bankAccountBalanceSummary.UpdatedOn,
                
                OwnerEntityId = rd.OwnerEntityId,
                OwnerAirslipUserType = rd.OwnerAirslipUserType,
                ViewerEntityId = rd.ViewerEntityId,
                ViewerAirslipUserType = rd.ViewerAirslipUserType,
                PermissionType = rd.PermissionType,
                Allowed = rd.Allowed,
                AuditInformation = integration.AuditInformation,
                EntityStatus = integration.EntityStatus
            };

        EntitySearchResponse<AccountBalanceReportModel> searchResults = await _entitySearch
            .GetSearchResults(qBalance, query, 
                new List<SearchFilterModel>
                {
                    new(nameof(AccountBalanceReportQuery.OwnerEntityId), query.OwnerEntityId),
                    new(nameof(AccountBalanceReportQuery.OwnerAirslipUserType), query.OwnerAirslipUserType.ToString()),
                    new(nameof(AccountBalanceReportQuery.ViewerEntityId), _userToken.EntityId),
                    new(nameof(AccountBalanceReportQuery.ViewerAirslipUserType), _userToken.AirslipUserType.ToString()),
                    new(nameof(AccountBalanceReportQuery.PermissionType), PermissionType.Banking.ToString()),
                    new(nameof(AccountBalanceReportQuery.Allowed), true)
                });
        
        return searchResults;
    }

    public Task<IResponse> Download(OwnedDataSearchModel query)
    {
        return Execute(query);
    }
}