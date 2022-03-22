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
    
    public async Task<IResponse> GetDebitsAndCredits(int year, string? integrationId)
    {
        integrationId = string.IsNullOrWhiteSpace(integrationId) ? null : integrationId;
        
        IQueryable<DebitsAndCreditsByYear> q = _context
            .Set<DebitsAndCreditsByYear>()
            .FromSqlRaw("dbo.GetCreditsAndDebitsByYear @Year = {0}, @EntityId = {1}, @AirslipUserType = {2}, @IntegrationId = {3}",
                year, 
                _userToken.EntityId,
                _userToken.AirslipUserType,
                integrationId == null ? DBNull.Value : integrationId);

        List<DebitsAndCreditsByYear> metrics = await q.ToListAsync();
        DateTimeFormatInfo formatter = CultureInfo.CurrentCulture.DateTimeFormat;
        DashboardGraphSeriesModel result = new(year,
            new []
            {
                new Series("Receivables", 
                    metrics.Select(o => new TimelyMetric(o.Month, formatter.GetAbbreviatedMonthName(o.Month),
                    o.TotalCredit, PeriodType.Month)),
                    metrics.Select( o=> o.TotalCredit.ToPositiveCurrency())
                    
                    ),
             new Series("Payables", metrics.Select(o => new TimelyMetric(o.Month, 
                 formatter.GetAbbreviatedMonthName(o.Month),
                 o.TotalDebit, PeriodType.Month)),
                 metrics.Select( o=> o.TotalDebit.ToPositiveCurrency()))   
            }
        );
        
        return result;
    }
}