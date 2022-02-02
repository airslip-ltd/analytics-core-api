using Airslip.Analytics.Core.Entities.Unmapped;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Interfaces;
using Airslip.Common.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Airslip.Analytics.Services.SqlServer.Implementations;

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
    
    public async Task<IResponse> GetRevenueAndRefunds(int year)
    {
        IQueryable<RevenueAndRefundsByYear> q = _context
            .Set<RevenueAndRefundsByYear>()
            .FromSqlRaw("dbo.GetRevenueAndRefundsByYear @Year = {0}, @EntityId = {1}, @AirslipUserType = {2}",
                year, 
                _userToken.EntityId,
                _userToken.AirslipUserType);

        List<RevenueAndRefundsByYear> metrics = await q.ToListAsync();
        DateTimeFormatInfo formatter = CultureInfo.CurrentCulture.DateTimeFormat;
        RevenueAndRefundsByYearModel result = new(year,
            new []
            {
                new Series("Revenue", 
                    metrics.Select(o => new TimelyMetric(o.Month, formatter.GetAbbreviatedMonthName(o.Month),
                    o.TotalSales, PeriodType.Month)),
                    metrics.Select( o=> o.TotalSales)
                    
                    ),
             new Series("Refunds", metrics.Select(o => new TimelyMetric(o.Month, 
                 formatter.GetAbbreviatedMonthName(o.Month),
                 o.TotalRefunds, PeriodType.Month)),
                 metrics.Select( o=> o.TotalRefunds))   
            }
        );
        
        return result;
    }
}