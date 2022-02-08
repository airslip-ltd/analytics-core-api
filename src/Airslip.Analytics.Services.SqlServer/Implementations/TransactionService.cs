using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Extensions;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Interfaces;
using Airslip.Common.Utilities.Extensions;
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
    
    public async Task<IResponse> GetBankingTransactions(int limit, string? accountId)
    {
        IQueryable<TransactionSummaryModel> qBalance = from bankTransaction in _context.BankTransactions
            join bankAccount in _context.BankAccounts on bankTransaction.AccountId equals bankAccount.AccountId
            where bankTransaction.EntityId.Equals(_userToken.EntityId)
            where bankTransaction.AirslipUserType == _userToken.AirslipUserType
            where accountId == null || bankTransaction.AccountId.Equals(accountId)
            orderby bankTransaction.CapturedDate descending 
            select new TransactionSummaryModel
            (
                bankTransaction.Id,
                bankTransaction.BankId,
                bankTransaction.Amount.ToCurrency(),
                bankTransaction.CurrencyCode ?? bankAccount.CurrencyCode,
                bankTransaction.CapturedDate,
                bankTransaction.Description
            );

        return new SimpleListResponse<TransactionSummaryModel>(await qBalance.Take(limit).ToListAsync());
    }

    public async Task<IResponse> GetCommerceTransactions(int limit, string? accountId)
    {
        IQueryable<TransactionSummaryModel> qBalance = from merchantTransaction in _context.MerchantTransactions
            join merchantAccount in _context.MerchantAccounts on merchantTransaction.AccountId equals merchantAccount.Id
            where merchantTransaction.EntityId.Equals(_userToken.EntityId)
            where merchantTransaction.AirslipUserType == _userToken.AirslipUserType
            where accountId == null || merchantTransaction.AccountId.Equals(accountId)
            orderby merchantTransaction.Datetime descending 
            select new TransactionSummaryModel
            (
                merchantTransaction.Id,
                merchantAccount.Provider,
                merchantTransaction.Total.ToCurrency(),
                merchantTransaction.CurrencyCode,
                merchantTransaction.Datetime.Value.ToUnixTimeMilliseconds(),
                merchantTransaction.Description
            );

        return new SimpleListResponse<TransactionSummaryModel>(await qBalance.Take(limit).ToListAsync());
    }

    public async Task<IResponse> GetMerchantAccounts()
    {
        IQueryable<MerchantAccountSummaryModel> qBalance = from merchantAccount in _context.MerchantAccounts
            where merchantAccount.EntityId.Equals(_userToken.EntityId)
            where merchantAccount.AirslipUserType == _userToken.AirslipUserType
            orderby merchantAccount.Name descending 
            select new MerchantAccountSummaryModel()
                {
                    Id = merchantAccount.Id,
                    Name = merchantAccount.Name,
                    Provider = merchantAccount.Provider,
                    AuthenticationState = merchantAccount.AuthenticationState
                };

        return new SimpleListResponse<MerchantAccountSummaryModel>(await qBalance.ToListAsync());
    }
}