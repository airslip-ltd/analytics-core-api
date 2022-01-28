using Airslip.Analytics.Core.Entities.Unmapped;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Interfaces;
using Airslip.Common.Utilities.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airslip.Analytics.Services.SqlServer.Implementations;

public class DashboardSnapshotService : IDashboardSnapshotService
{
    private readonly DbContext _context;

    private static Dictionary<DashboardSnapshotType, string> procedureNames = new()
    {
        { DashboardSnapshotType.TotalSales, "dbo.GetTotalSalesSnapshot" },
        { DashboardSnapshotType.TotalRefunds, "dbo.GetTotalRefundsSnapshot" }
    };

    private readonly UserToken _userToken;

    public DashboardSnapshotService(IContext context, ITokenDecodeService<UserToken> userToken)
    {
        if (context is not DbContext dbContext) 
            throw new ArgumentException("Invalid context", nameof(context));
        _userToken = userToken.GetCurrentToken();
        _context = dbContext;
    }
    
    public async Task<IResponse> GetSnapshotFor(DashboardSnapshotType dashboardSnapshotType, int dayRange, int statRange)
    {
        string procName = procedureNames[dashboardSnapshotType];
        
        IQueryable<DashboardMetricSnapshot> q = _context
            .Set<DashboardMetricSnapshot>()
            .FromSqlRaw($"{procName} @DayRange = {{0}}, @StatRange = {{1}}, @EntityId = {{2}}, @AirslipUserType = {{3}}",
                dayRange, 
                statRange,
                _userToken.EntityId,
                _userToken.AirslipUserType);

        List<DashboardMetricSnapshot> metrics = await q.ToListAsync();

        DashboardMetricSnapshot primary = metrics.First();
        DashboardMetricSnapshot secondary = metrics.Skip(1).First();

        double movement = secondary.Balance switch
        {
            0 when primary.Balance == 0 => 0,
            0 when primary.Balance != 0 => 100,
            _ => (primary.Balance - secondary.Balance) / secondary.Balance * 100
        };

        DashboardSnapshotModel result = new()
        {
            Balance = primary.Balance,
            DayRange = dayRange,
            TimeStamp = primary.MetricDate.ToUnixTimeMilliseconds(),
            Movement = movement,
            Metrics = metrics.OrderBy(o => o.MetricDate).Select(o => new SnapshotMetric()
            {
                Balance = o.Balance,
                TimeStamp = o.MetricDate.ToUnixTimeMilliseconds()
            }).ToList()
        };

        return result;
    }
}