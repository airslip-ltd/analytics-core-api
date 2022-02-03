using Airslip.Analytics.Core.Extensions;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Failures;
using Airslip.Common.Types.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airslip.Analytics.Services.SqlServer.Implementations;

public class BalanceService : IBalanceService
{
    private readonly SqlServerContext _context;
    private readonly UserToken _userToken;

    public BalanceService(IContext context, ITokenDecodeService<UserToken> tokenDecodeService)
    {
        if (context is not SqlServerContext dbContext) 
            throw new ArgumentException("Invalid context", nameof(context));
        _context = dbContext;
        _userToken = tokenDecodeService.GetCurrentToken();
    }
    
    public async Task<IResponse> GetCurrentBalance()
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