using Airslip.Analytics.Core.Entities.Unmapped;
using Airslip.Analytics.Core.Extensions;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Failures;
using Airslip.Common.Types.Interfaces;
using Airslip.Common.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airslip.Analytics.Services.SqlServer.Implementations;

public class DashboardSnapshotService : IDashboardSnapshotService
{
    private readonly SqlServerContext _context;

    private static readonly Dictionary<DashboardSnapshotType, string> procedureNames = new()
    {
        { DashboardSnapshotType.TotalSales, "dbo.GetTotalSalesSnapshot" },
        { DashboardSnapshotType.TotalRefunds, "dbo.GetTotalRefundsSnapshot" }
    };

    private readonly UserToken _userToken;

    public DashboardSnapshotService(IContext context, ITokenDecodeService<UserToken> userToken)
    {
        if (context is not SqlServerContext dbContext) 
            throw new ArgumentException("Invalid context", nameof(context));
        _context = dbContext;
        _userToken = userToken.GetCurrentToken();
    }
    
    public async Task<IResponse> GetSnapshotFor(DashboardSnapshotType dashboardSnapshotType, int dayRange, 
        int statRange, string? accountId)
    {
        IResponse result;
        
        switch (dashboardSnapshotType)
        {
            case DashboardSnapshotType.CurrentBalance:
                result = await _getCurrentBalance();
                break;
            default:
                result = await _getGenericSnapshot(dashboardSnapshotType, dayRange, statRange, accountId);
                break;
        }

        return result;
    }

    private async Task<IResponse> _getGenericSnapshot(DashboardSnapshotType dashboardSnapshotType, int dayRange,
        int statRange, string? accountId)
    {
        string procName = procedureNames[dashboardSnapshotType];
        
        IQueryable<DashboardMetricSnapshot> q = _context
            .Set<DashboardMetricSnapshot>()
            .FromSqlRaw($"{procName} @DayRange = {{0}}, @StatRange = {{1}}, @EntityId = {{2}}, @AirslipUserType = {{3}}, @AccountId = {{4}}",
                dayRange, 
                statRange,
                _userToken.EntityId,
                _userToken.AirslipUserType,
                accountId == null ? DBNull.Value : accountId);

        List<DashboardMetricSnapshot> metrics = await q.ToListAsync();

        DashboardMetricSnapshot primary = metrics.First();
        DashboardMetricSnapshot secondary = metrics.Skip(1).First();

        double movement = secondary.Balance switch
        {
            0 when primary.Balance == 0 => 0,
            0 when primary.Balance != 0 => 100,
            _ => (primary.Balance - secondary.Balance) / secondary.Balance * 100
        };

        return new DashboardSnapshotModel()
        {
            Balance = primary.Balance.ToPositiveCurrency(),
            DayRange = dayRange,
            TimeStamp = primary.MetricDate.ToUnixTimeMilliseconds(),
            Movement = movement,
            Metrics = metrics.OrderBy(o => o.MetricDate)
                .Select(o => new SnapshotMetric(o.MetricDate.ToUnixTimeMilliseconds(), 
                    o.Balance.ToPositiveCurrency())
                ).ToList()
        };
    }
    
    private async Task<IResponse> _getCurrentBalance()
    {
        IQueryable<DashboardSnapshotModel> qBalance = from businessBalance in _context.BankBusinessBalances
            where businessBalance.EntityId.Equals(_userToken.EntityId)
            where businessBalance.AirslipUserType == _userToken.AirslipUserType
            select new DashboardSnapshotModel
            {
                Balance = businessBalance.Balance.ToPositiveCurrency(),
                TimeStamp = businessBalance.TimeStamp,
                Movement = businessBalance.Movement
            };
        
        IQueryable<SnapshotMetric> qSnapshot = from accountBalanceSnapshot in _context.BankBusinessBalanceSnapshots
            where accountBalanceSnapshot.EntityId.Equals(_userToken.EntityId)
            where accountBalanceSnapshot.AirslipUserType == _userToken.AirslipUserType
            orderby accountBalanceSnapshot.TimeStamp
            select new SnapshotMetric(accountBalanceSnapshot.TimeStamp, accountBalanceSnapshot.Balance.ToPositiveCurrency());

        DashboardSnapshotModel? response = await qBalance.FirstOrDefaultAsync();

        if (response == null) return new NotFoundResponse("BusinessBalance", _userToken.EntityId);

        response.Metrics = await qSnapshot
            .Take(10)
            .ToListAsync();

        while (response.Metrics.Count < 10)
        {
            response.Metrics.Insert(0, new SnapshotMetric(0, 0));
        }
        
        return response;
    }
}