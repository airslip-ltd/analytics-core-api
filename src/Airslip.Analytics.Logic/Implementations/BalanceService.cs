using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Core.Extensions;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Services.SqlServer;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Airslip.Analytics.Logic.Implementations;

public class BalanceService : IBalanceService
{
    private readonly SqlServerContext _context;
    private readonly UserToken _userToken;

    public BalanceService(IContext context, ITokenDecodeService<UserToken> tokenDecodeService)
    {
        if (context is not SqlServerContext dbContext) 
            throw new ArgumentException("Invalid context", nameof(context));
        _context = dbContext;
        _userToken = tokenDecodeService.GetCurrentToken();
    }
    
    public async Task<IResponse> GetAccountBalances()
    {
        IQueryable<AccountBalanceSummaryModel> qBalance = from integration in _context.Integrations
            join bankAccountBalanceSummary in _context.BankAccountBalanceSummary on integration.Id 
                equals bankAccountBalanceSummary.AccountId
                from accountDetail in _context.IntegrationAccountDetails
                    .Where(o => o.IntegrationId.Equals(integration.Id))
            where integration.EntityId.Equals(_userToken.EntityId)
            where integration.AirslipUserType == _userToken.AirslipUserType
            where integration.IntegrationType == IntegrationType.Banking
            select new AccountBalanceSummaryModel
            (
                accountDetail.AccountId,
                integration.IntegrationProviderId,
                accountDetail.AccountStatus,
                accountDetail.SortCode,
                accountDetail.AccountNumber,
                accountDetail.CurrencyCode,
                bankAccountBalanceSummary.Balance.ToCurrency(),
                bankAccountBalanceSummary.UpdatedOn
            );

        return new AccountBalanceSummaryResponse(await qBalance.ToListAsync());
    }
}