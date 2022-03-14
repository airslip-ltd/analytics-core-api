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
        
        public async Task<IResponse> Execute(OwnedDataSearchModel query)
        {
            IQueryable<CommerceTransactionReportQuery> q = 
                from rd in _context.RelationshipDetails
                from item in _context.MerchantTransactions
                    .Where(o => o.EntityId.Equals(rd.OwnerEntityId) && o.AirslipUserType == rd.OwnerAirslipUserType)
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
                    EntityStatus = item.EntityStatus,
                    TimeStamp = item.TimeStamp,
                    UserId = item.UserId,
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
                    StoreLocationId = item.StoreLocationId ,
                    
                    EntityId = rd.OwnerEntityId,
                    AirslipUserType = rd.OwnerAirslipUserType,
                    ViewerEntityId = rd.ViewerEntityId,
                    ViewerAirslipUserType = rd.ViewerAirslipUserType,
                    PermissionType = rd.PermissionType,
                    Allowed = rd.Allowed
                };
            
            EntitySearchResponse<CommerceTransactionReportResponse> searchResults = await _entitySearch
                .GetSearchResults(q, query, 
                    new List<SearchFilterModel>
                    {
                        new(nameof(BankTransactionReportQuery.EntityId), query.OwnerEntityId),
                        new(nameof(BankTransactionReportQuery.AirslipUserType), query.OwnerAirslipUserType.ToString()),
                        new(nameof(BankTransactionReportQuery.ViewerEntityId), _userToken.EntityId),
                        new(nameof(BankTransactionReportQuery.ViewerAirslipUserType), _userToken.AirslipUserType.ToString()),
                        new(nameof(BankTransactionReportQuery.PermissionType), PermissionType.Commerce.ToString()),
                        new(nameof(BankTransactionReportQuery.Allowed), true)
                    });
            
            return searchResults;
        }
    }
}