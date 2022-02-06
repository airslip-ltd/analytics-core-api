using Airslip.Analytics.Core.Extensions;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Failures;
using Airslip.Common.Types.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Airslip.Analytics.Services.SqlServer.Implementations;

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
        IQueryable<AccountBalanceSummaryModel> qBalance = from bankAccount in _context.BankAccounts
            join bankAccountBalanceSummary in _context.BankAccountBalanceSummary on bankAccount.Id 
                equals bankAccountBalanceSummary.AccountId 
            where bankAccount.EntityId.Equals(_userToken.EntityId)
            where bankAccount.AirslipUserType == _userToken.AirslipUserType
            select new AccountBalanceSummaryModel
            (
                bankAccount.InstitutionId,
                bankAccount.AccountStatus,
                bankAccount.SortCode,
                bankAccount.AccountNumber,
                bankAccountBalanceSummary.Balance.ToCurrency(),
                bankAccountBalanceSummary.UpdatedOn
            );

        return new AccountBalanceSummaryResponse(await qBalance.ToListAsync());
    }
}