using Airslip.Analytics.Core.Extensions;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Airslip.Analytics.Services.SqlServer.Implementations;

public class TransactionService : ITransactionService
{
    private readonly SqlServerContext _context;
    private readonly UserToken _userToken;

    public TransactionService(IContext context, ITokenDecodeService<UserToken> tokenDecodeService)
    {
        if (context is not SqlServerContext dbContext) 
            throw new ArgumentException("Invalid context", nameof(context));
        _context = dbContext;
        _userToken = tokenDecodeService.GetCurrentToken();
    }
    
    public async Task<IResponse> GetAccountTransactions(int limit, string? accountId)
    {
        IQueryable<BankTransactionSummaryModel> qBalance = from bankTransaction in _context.BankTransactions
            join bankAccount in _context.BankAccounts on bankTransaction.AccountId equals bankAccount.AccountId
            where bankTransaction.EntityId.Equals(_userToken.EntityId)
            where bankTransaction.AirslipUserType == _userToken.AirslipUserType
            where accountId == null || bankTransaction.AccountId.Equals(accountId)
            orderby bankTransaction.CapturedDate descending 
            select new BankTransactionSummaryModel
            (
                bankTransaction.Id,
                bankTransaction.BankId,
                bankTransaction.Amount.ToCurrency(),
                bankTransaction.CurrencyCode ?? bankAccount.CurrencyCode,
                bankTransaction.CapturedDate,
                bankTransaction.Description
            );

        return new BankTransactionSummaryResponse(await qBalance.Take(limit).ToListAsync());
    }
}