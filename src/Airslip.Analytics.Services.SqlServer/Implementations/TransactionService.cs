using Airslip.Analytics.Core.Enums;
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
            join bankAccount in _context.Integrations on bankTransaction.AccountId equals bankAccount.Id
            where bankTransaction.EntityId.Equals(_userToken.EntityId)
            where bankTransaction.AirslipUserType == _userToken.AirslipUserType
            where accountId == null || bankTransaction.AccountId.Equals(accountId)
            orderby bankTransaction.CapturedDate descending 
            select new TransactionSummaryModel
            (
                bankTransaction.Id,
                bankTransaction.BankId,
                bankTransaction.Amount.ToCurrency(),
                bankTransaction.CurrencyCode,
                bankTransaction.CapturedDate,
                bankTransaction.Description
            );

        return new SimpleListResponse<TransactionSummaryModel>(await qBalance.Take(limit).ToListAsync());
    }

    public async Task<IResponse> GetCommerceTransactions(int limit, string? accountId)
    {
        IQueryable<TransactionSummaryModel> qBalance = from merchantTransaction in _context.MerchantTransactions
            join merchantAccount in _context.Integrations on merchantTransaction.AccountId equals merchantAccount.Id
            where merchantTransaction.EntityId.Equals(_userToken.EntityId)
            where merchantTransaction.AirslipUserType == _userToken.AirslipUserType
            where accountId == null || merchantTransaction.AccountId.Equals(accountId)
            orderby merchantTransaction.Datetime descending 
            select new TransactionSummaryModel
            (
                merchantTransaction.Id,
                merchantAccount.IntegrationProviderId,
                merchantTransaction.Total.ToCurrency(),
                merchantTransaction.CurrencyCode,
                merchantTransaction.Datetime.Value.ToUnixTimeMilliseconds(),
                merchantTransaction.Description
            );

        return new SimpleListResponse<TransactionSummaryModel>(await qBalance.Take(limit).ToListAsync());
    }

    public async Task<IResponse> GetMerchantAccounts()
    {
        IQueryable<IntegrationSummaryModel> qBalance = from integration in _context.Integrations
            where integration.EntityId.Equals(_userToken.EntityId)
            where integration.AirslipUserType == _userToken.AirslipUserType
            where integration.IntegrationType == IntegrationType.Commerce
            orderby integration.Name descending 
            select new IntegrationSummaryModel
                {
                    Id = integration.Id,
                    Name = integration.Name,
                    IntegrationProviderId = integration.IntegrationProviderId,
                    AuthenticationState = integration.AuthenticationState
                };

        return new SimpleListResponse<IntegrationSummaryModel>(await qBalance.ToListAsync());
    }
}