using Airslip.Analytics.Core.Entities.Unmapped;
using Airslip.Analytics.Core.Enums;
using Airslip.Analytics.Core.Extensions;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Services.SqlServer;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Failures;
using Airslip.Common.Types.Interfaces;
using Airslip.Common.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Airslip.Analytics.Logic.Implementations;

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
    
    public async Task<IResponse> GetSnapshotFor(OwnedSnapshotSearchModel query, 
        DashboardSnapshotType dashboardSnapshotType, int dayRange, int statRange, string? integrationId)
    {
        IResponse result;
        
        switch (dashboardSnapshotType)
        {
            case DashboardSnapshotType.CurrentBalance:
                result = await _getCurrentBalance(query);
                break;
            default:
                result = await _getGenericSnapshot(query, dashboardSnapshotType, dayRange, statRange, integrationId);
                break;
        }

        return result;
    }

    private async Task<IResponse> _getGenericSnapshot(OwnedSnapshotSearchModel query,
        DashboardSnapshotType dashboardSnapshotType, int dayRange,
        int statRange, string? integrationId)
    {
        string procName = procedureNames[dashboardSnapshotType];
        
        IQueryable<DashboardMetricSnapshot> q = _context
            .Set<DashboardMetricSnapshot>()
            .FromSqlRaw($"{procName} @DayRange = {{0}}, @StatRange = {{1}}, @EntityId = {{2}}, @AirslipUserType = {{3}}, @IntegrationId = {{4}}",
                dayRange, 
                statRange,
                _userToken.EntityId,
                _userToken.AirslipUserType,
                integrationId == null ? DBNull.Value : integrationId);

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
    
    private async Task<IResponse> _getCurrentBalance(OwnedSnapshotSearchModel query)
    {
        IQueryable<DashboardSnapshotModel> qBalance = from rd in _context.RelationshipDetails
            from rh in _context.RelationshipHeaders
                .Where(o => o.Id.Equals(rd.RelationshipHeaderId) && o.EntityStatus == EntityStatus.Active)
            from businessBalance in _context.BankBusinessBalances
                .Where(o => o.EntityId == rd.OwnerEntityId && o.AirslipUserType == rd.OwnerAirslipUserType)
            where rd.ViewerEntityId.Equals(_userToken.EntityId) 
                  && rd.ViewerAirslipUserType == _userToken.AirslipUserType 
                  && rd.Allowed
                  && rd.PermissionType == PermissionType.Banking.ToString()
                  && rd.OwnerEntityId == query.OwnerEntityId
                  && rd.OwnerAirslipUserType == query.OwnerAirslipUserType 
            select new DashboardSnapshotModel
            {
                Balance = businessBalance.Balance.ToPositiveCurrency(),
                TimeStamp = businessBalance.TimeStamp,
                Movement = businessBalance.Movement
            };
        
        IQueryable<SnapshotMetric> qSnapshot = from rd in _context.RelationshipDetails
            from rh in _context.RelationshipHeaders
                .Where(o => o.Id.Equals(rd.RelationshipHeaderId) && 
                            o.EntityStatus == EntityStatus.Active)
            from accountBalanceSnapshot in _context.BankBusinessBalanceSnapshots
                .Where(o => o.EntityId == rd.OwnerEntityId && 
                            o.AirslipUserType == rd.OwnerAirslipUserType)
            where rd.ViewerEntityId.Equals(_userToken.EntityId) 
                  && rd.ViewerAirslipUserType == _userToken.AirslipUserType 
                  && rd.Allowed
                  && rd.PermissionType == PermissionType.Banking.ToString()
                  && rd.OwnerEntityId == query.OwnerEntityId
                  && rd.OwnerAirslipUserType == query.OwnerAirslipUserType 
            orderby accountBalanceSnapshot.TimeStamp
            select new SnapshotMetric(accountBalanceSnapshot.TimeStamp, accountBalanceSnapshot.Balance.ToPositiveCurrency());

        DashboardSnapshotModel? response = await qBalance.FirstOrDefaultAsync();

        if (response == null) return new NotFoundResponse("BusinessBalance", query.OwnerEntityId);

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