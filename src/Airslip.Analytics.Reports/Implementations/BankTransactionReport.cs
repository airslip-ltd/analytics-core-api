using Airslip.Analytics.Reports.Data;
using Airslip.Analytics.Reports.Interfaces;
using Airslip.Analytics.Reports.Models;
using Airslip.Analytics.Services.SqlServer;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Interfaces;

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
        
        public async Task<IResponse> Execute(EntitySearchQueryModel query)
        {
            IQueryable<BankTransactionReportQuery> q = from item in _context.BankTransactions
                select new BankTransactionReportQuery
                {
                    Id = item.Id,
                    Amount = item.Amount,
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
                    EntityId = item.EntityId,
                    EntityStatus = item.EntityStatus,
                    ProprietaryCode = item.ProprietaryCode,
                    TimeStamp = item.TimeStamp,
                    TransactionHash = item.TransactionHash,
                    TransactionIdentifier = item.TransactionIdentifier,
                    UserId = item.UserId,
                    AirslipUserType = item.AirslipUserType,
                    BankTransactionId = item.BankTransactionId,
                    IsoFamilyCode = item.IsoFamilyCode,
                    LastCardDigits = item.LastCardDigits
                };
            
            EntitySearchResponse<BankTransactionReportResponse> searchResults = await _entitySearch
                .GetSearchResults(q, query, 
                    new List<SearchFilterModel>()
                    {
                        new(nameof(BankTransactionReportQuery.EntityId), _userToken.EntityId),
                        new(nameof(BankTransactionReportQuery.AirslipUserType), _userToken.AirslipUserType.ToString())
                    });
            
            return searchResults;
        }
    }
}