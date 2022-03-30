using Airslip.Analytics.Core.Entities.Unmapped;
using Airslip.Analytics.Core.Extensions;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Airslip.Analytics.Logic.Implementations;

public class RevenueAndRefundsService : IRevenueAndRefundsService
{
    private readonly DbContext _context;
    private readonly UserToken _userToken;

    public RevenueAndRefundsService(IContext context, ITokenDecodeService<UserToken> userToken)
    {
        if (context is not DbContext dbContext) 
            throw new ArgumentException("Invalid context", nameof(context));
        _userToken = userToken.GetCurrentToken();
        _context = dbContext;
    }
    
    public async Task<IResponse> GetRevenueAndRefunds(OwnedSnapshotSearchModel query, int year, string? integrationId)
    {
        IQueryable<RevenueAndRefundsByYear> q = _context
            .Set<RevenueAndRefundsByYear>()
            .FromSqlRaw("dbo.GetRevenueAndRefundsByYear @Year = {0}, @ViewerEntityId = {1}, @ViewerAirslipUserType = {2}, @OwnerEntityId = {3}, @OwnerAirslipUserType = {4}, @IntegrationId = {5}",
                year, 
                _userToken.EntityId,
                _userToken.AirslipUserType,
                query.OwnerEntityId,
                query.OwnerAirslipUserType,
                integrationId == null ? DBNull.Value : integrationId);

        List<RevenueAndRefundsByYear> metrics = await q.ToListAsync();
        DateTimeFormatInfo formatter = CultureInfo.CurrentCulture.DateTimeFormat;
        DashboardGraphSeriesModel result = new(year,
            new []
            {
                new Series("Revenue", 
                    metrics.Select(o => new TimelyMetric(o.Month, formatter.GetAbbreviatedMonthName(o.Month),
                    o.TotalSales, PeriodType.Month)),
                    metrics.Select( o=> o.TotalSales.ToPositiveCurrency())
                    
                    ),
             new Series("Refunds", metrics.Select(o => new TimelyMetric(o.Month, 
                 formatter.GetAbbreviatedMonthName(o.Month),
                 o.TotalRefunds, PeriodType.Month)),
                 metrics.Select( o=> o.TotalRefunds.ToPositiveCurrency()))   
            }
        );
        
        return result;
    }
}