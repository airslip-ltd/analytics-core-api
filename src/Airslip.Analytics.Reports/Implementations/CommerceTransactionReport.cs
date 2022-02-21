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
    public class CommerceTransactionReport : ICommerceTransactionReport
    {
        private readonly IEntitySearch<CommerceTransactionReportResponse> _entitySearch;
        private readonly UserToken _userToken;
        private readonly SqlServerContext _context;

        public CommerceTransactionReport(IEntitySearch<CommerceTransactionReportResponse> entitySearch, IContext context,
            ITokenDecodeService<UserToken> tokenDecodeService)
        {
            _context = context as SqlServerContext ?? throw new NotImplementedException();
            _entitySearch = entitySearch;
            _userToken = tokenDecodeService.GetCurrentToken();
        }
        
        public async Task<IResponse> Execute(EntitySearchQueryModel entitySearchQueryModel)
        {
            IQueryable<CommerceTransactionReportQuery> q = from item in _context.MerchantTransactions
                select new CommerceTransactionReportQuery
                {
                    Id = item.Id,
                    Day = item.Day,
                    Description = item.Description,
                    Month = item.Month,
                    Year = item.Year,
                    AccountId = item.AccountId,
                    CurrencyCode = item.CurrencyCode,
                    DataSource = item.DataSource,
                    EntityId = item.EntityId,
                    EntityStatus = item.EntityStatus,
                    TimeStamp = item.TimeStamp,
                    UserId = item.UserId,
                    AirslipUserType = item.AirslipUserType,
                    Date = item.Date,
                    Datetime = item.Datetime,
                    Number = item.Number,
                    Source = item.Source,
                    Store = item.Store,
                    Subtotal = item.Subtotal,
                    Till = item.Till,
                    Time = item.Time,
                    Total = item.Total,
                    CustomerEmail = item.CustomerEmail,
                    InternalId = item.InternalId,
                    OnlinePurchase = item.OnlinePurchase,
                    OperatorName = item.OperatorName,
                    RefundCode = item.RefundCode,
                    ServiceCharge = item.ServiceCharge,
                    StoreAddress = item.StoreAddress,
                    TrackingId = item.TrackingId,
                    TransactionNumber = item.TransactionNumber,
                    StoreLocationId = item.StoreLocationId
                };
            
            EntitySearchResponse<CommerceTransactionReportResponse> searchResults = await _entitySearch
                .GetSearchResults(q, entitySearchQueryModel, 
                    new List<SearchFilterModel>
                    {
                        new(nameof(CommerceTransactionReportQuery.EntityId), _userToken.EntityId),
                        new(nameof(CommerceTransactionReportQuery.AirslipUserType), _userToken.AirslipUserType.ToString())
                    });
            
            return searchResults;
        }
    }
}