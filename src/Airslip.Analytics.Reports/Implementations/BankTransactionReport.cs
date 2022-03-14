using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Reports.Data;
using Airslip.Analytics.Reports.Interfaces;
using Airslip.Analytics.Reports.Models;
using Airslip.Analytics.Services.SqlServer;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Interfaces;
using JetBrains.Annotations;

namespace Airslip.Analytics.Reports.Implementations
{
    public class BankTransactionReport : IBankTransactionReport
    {
        private readonly IEntitySearch<BankTransactionReportResponse> _entitySearch;
        private readonly UserToken _userToken;
        private readonly SqlServerContext _context;

        public BankTransactionReport(IEntitySearch<BankTransactionReportResponse> entitySearch, IContext context,
            ITokenDecodeService<UserToken> tokenDecodeService)
        {
            _context = context as SqlServerContext ?? throw new NotImplementedException();
            _entitySearch = entitySearch;
            _userToken = tokenDecodeService.GetCurrentToken();
        }
        
        public async Task<IResponse> Execute(OwnedDataSearchModel query)
        {
            IQueryable<BankTransactionReportQuery> q = from rd in _context.RelationshipDetails
                from item in _context.BankTransactions
                    .Where(o => o.EntityId.Equals(rd.OwnerEntityId) && o.AirslipUserType == rd.OwnerAirslipUserType) 
                join bank in _context.Banks on item.BankId equals bank.Id
                select new BankTransactionReportQuery
                {
                    Id = item.Id,
                    Amount = item.Amount,
                    TradingName = bank.TradingName,
                    Day = item.Day,
                    Description = item.Description,
                    Month = item.Month,
                    Reference = item.Reference,
                    Year = item.Year,
                    AccountId = item.AccountId,
                    AddressLine = item.AddressLine,
                    AuthorisedDate = item.AuthorisedDate,
                    BankId = item.BankId,
                    CapturedDate = item.CapturedDate,
                    CurrencyCode = item.CurrencyCode,
                    DataSource = item.DataSource,
                    EmailAddress = item.EmailAddress,
                    EntityStatus = item.EntityStatus,
                    ProprietaryCode = item.ProprietaryCode,
                    TimeStamp = item.TimeStamp,
                    TransactionHash = item.TransactionHash,
                    TransactionIdentifier = item.TransactionIdentifier,
                    UserId = item.UserId,
                    BankTransactionId = item.BankTransactionId,
                    IsoFamilyCode = item.IsoFamilyCode,
                    LastCardDigits = item.LastCardDigits,
                    
                    EntityId = rd.OwnerEntityId,
                    AirslipUserType = rd.OwnerAirslipUserType,
                    ViewerEntityId = rd.ViewerEntityId,
                    ViewerAirslipUserType = rd.ViewerAirslipUserType,
                    PermissionType = rd.PermissionType,
                    Allowed = rd.Allowed
                };
            
            EntitySearchResponse<BankTransactionReportResponse> searchResults = await _entitySearch
                .GetSearchResults(q, query, 
                    new List<SearchFilterModel>
                    {
                        new(nameof(BankTransactionReportQuery.EntityId), query.OwnerEntityId),
                        new(nameof(BankTransactionReportQuery.AirslipUserType), query.OwnerAirslipUserType.ToString()),
                        new(nameof(BankTransactionReportQuery.ViewerEntityId), _userToken.EntityId),
                        new(nameof(BankTransactionReportQuery.ViewerAirslipUserType), _userToken.AirslipUserType.ToString()),
                        new(nameof(BankTransactionReportQuery.PermissionType), PermissionType.Banking.ToString()),
                        new(nameof(BankTransactionReportQuery.Allowed), true)
                    });
            
            return searchResults;
        }
    }
}