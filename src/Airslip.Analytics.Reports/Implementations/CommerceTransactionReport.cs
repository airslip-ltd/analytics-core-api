using Airslip.Analytics.Core.Enums;
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

public class CommerceTransactionReport : ICommerceTransactionReport
{
    private readonly IEntitySearch<CommerceTransactionReportModel> _entitySearch;
    private readonly IEntitySearch<MerchantTransactionModel> _entityDownload;
    private readonly UserToken _userToken;
    private readonly SqlServerContext _context;

    public CommerceTransactionReport(
        IEntitySearch<CommerceTransactionReportModel> entitySearch, 
        IEntitySearch<MerchantTransactionModel> entityDownload, 
        IContext context,
        ITokenDecodeService<UserToken> tokenDecodeService)
    {
        _context = context as SqlServerContext ?? throw new NotImplementedException();
        _entitySearch = entitySearch;
        _entityDownload = entityDownload;
        _userToken = tokenDecodeService.GetCurrentToken();
    }
        
    public async Task<IResponse> Execute(OwnedDataSearchModel query)
    {
        IQueryable<CommerceTransactionReportQuery> q = 
            from rd in _context.RelationshipDetails
            from item in _context.MerchantTransactions.Where(o => o.EntityId.Equals(rd.OwnerEntityId) && o.AirslipUserType == rd.OwnerAirslipUserType)
            select new CommerceTransactionReportQuery
            {
                Id = item.Id,
                Day = item.Day,
                Description = item.Description,
                Month = item.Month,
                Year = item.Year,
                IntegrationId = item.IntegrationId,
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
                OrderStatus = item.OrderStatus,
                PaymentStatus = item.PaymentStatus,
                TotalRefund = item.TotalRefund,
                    
                OwnerEntityId = rd.OwnerEntityId,
                OwnerAirslipUserType = rd.OwnerAirslipUserType,
                ViewerEntityId = rd.ViewerEntityId,
                ViewerAirslipUserType = rd.ViewerAirslipUserType,
                PermissionType = rd.PermissionType,
                Allowed = rd.Allowed
            };
            
        EntitySearchResponse<CommerceTransactionReportModel> searchResults = await _entitySearch
            .GetSearchResults(q, query, 
                new List<SearchFilterModel>
                {
                    new(nameof(CommerceTransactionReportQuery.OwnerEntityId), query.OwnerEntityId),
                    new(nameof(CommerceTransactionReportQuery.OwnerAirslipUserType), query.OwnerAirslipUserType.ToString()),
                    new(nameof(CommerceTransactionReportQuery.ViewerEntityId), _userToken.EntityId),
                    new(nameof(CommerceTransactionReportQuery.ViewerAirslipUserType), _userToken.AirslipUserType.ToString()),
                    new(nameof(CommerceTransactionReportQuery.PermissionType), PermissionType.Commerce.ToString()),
                    new(nameof(CommerceTransactionReportQuery.Allowed), true)
                });
            
        return searchResults;
    }

    public async Task<IResponse> Download(OwnedDataSearchModel query)
    {
        IQueryable<CommerceTransactionDownloadQuery> q = 
            from rd in _context.RelationshipDetails
            from item in _context.MerchantTransactions.Where(o => o.EntityId.Equals(rd.OwnerEntityId) && o.AirslipUserType == rd.OwnerAirslipUserType)
            select new CommerceTransactionDownloadQuery
            {
                Id = item.Id,
                Day = item.Day,
                Description = item.Description,
                Month = item.Month,
                Year = item.Year,
                IntegrationId = item.IntegrationId,
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
                OrderStatus = item.OrderStatus,
                PaymentStatus = item.PaymentStatus,
                TotalRefund = item.TotalRefund,
                    
                OwnerEntityId = rd.OwnerEntityId,
                OwnerAirslipUserType = rd.OwnerAirslipUserType,
                ViewerEntityId = rd.ViewerEntityId,
                ViewerAirslipUserType = rd.ViewerAirslipUserType,
                PermissionType = rd.PermissionType,
                Allowed = rd.Allowed,
                Products = item.Products,
                Refunds = item.Refunds
            };
            
        EntitySearchResponse<MerchantTransactionModel> searchResults = await _entityDownload
            .GetSearchResults(q, query, 
                new List<SearchFilterModel>
                {
                    new(nameof(CommerceTransactionDownloadQuery.OwnerEntityId), query.OwnerEntityId),
                    new(nameof(CommerceTransactionDownloadQuery.OwnerAirslipUserType), query.OwnerAirslipUserType.ToString()),
                    new(nameof(CommerceTransactionDownloadQuery.ViewerEntityId), _userToken.EntityId),
                    new(nameof(CommerceTransactionDownloadQuery.ViewerAirslipUserType), _userToken.AirslipUserType.ToString()),
                    new(nameof(CommerceTransactionDownloadQuery.PermissionType), PermissionType.Commerce.ToString()),
                    new(nameof(CommerceTransactionDownloadQuery.Allowed), true)
                });
            
        return searchResults;
    }
}