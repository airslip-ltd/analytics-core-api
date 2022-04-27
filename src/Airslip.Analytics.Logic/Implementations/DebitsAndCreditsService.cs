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

public class DebitsAndCreditsService : IDebitsAndCreditsService
{
    private readonly DbContext _context;
    private readonly UserToken _userToken;

    public DebitsAndCreditsService(IContext context, ITokenDecodeService<UserToken> userToken)
    {
        if (context is not DbContext dbContext) 
            throw new ArgumentException("Invalid context", nameof(context));
        _userToken = userToken.GetCurrentToken();
        _context = dbContext;
    }
    
    public async Task<IResponse> Execute(OwnedSeriesSearchModel query)
    {
        IQueryable<DebitsAndCreditsByYear> q = _context
            .Set<DebitsAndCreditsByYear>()
            .FromSqlRaw(@"dbo.GetCreditsAndDebitsByRange @Start = {0},
@End = {1}, 
@ViewerEntityId = {2}, 
@ViewerAirslipUserType = {3}, 
@OwnerEntityId = {4}, 
@OwnerAirslipUserType = {5}, 
@IntegrationId = {6}, 
@CurrencyCode = {7}",
                query.StartDate,
                query.EndDate,
                _userToken.EntityId,
                _userToken.AirslipUserType,
                query.OwnerEntityId,
                query.OwnerAirslipUserType,
                query.IntegrationId == null ? DBNull.Value : query.IntegrationId,
                query.CurrencyCode);

        List<DebitsAndCreditsByYear> metrics = await q.ToListAsync();
        DateTimeFormatInfo formatter = CultureInfo.CurrentCulture.DateTimeFormat;
        DashboardGraphSeriesModel result = new(query.StartDate, query.EndDate, query.CurrencyCode, 
            new []
            {
                new Series("Money In", 
                    metrics.Select(o => new TimelyMetric(o.Month, formatter.GetAbbreviatedMonthName(o.Month),
                    o.TotalCredit, PeriodType.Month)),
                    metrics.Select( o=> o.TotalCredit.ToPositiveCurrency())
                    ),
             new Series("Money Out", metrics.Select(o => new TimelyMetric(o.Month, 
                 formatter.GetAbbreviatedMonthName(o.Month),
                 o.TotalDebit, PeriodType.Month)),
                 metrics.Select( o=> o.TotalDebit.ToPositiveCurrency()))   
            }
        );
        
        return result;
    }
}