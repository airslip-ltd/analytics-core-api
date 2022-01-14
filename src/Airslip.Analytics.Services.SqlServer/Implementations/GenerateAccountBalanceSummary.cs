using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Airslip.Analytics.Services.SqlServer.Implementations;

public class GenerateAccountBalanceSummary : IAnalyticsProcess<AccountBalanceModel>
{
    private readonly DbContext _context;

    public GenerateAccountBalanceSummary(IContext context)
    {
        if (context is not DbContext dbContext) 
            throw new ArgumentException("Invalid context", nameof(context));
        _context = dbContext;
    }
    
    public Task Execute(AccountBalanceModel model)
    {
        if (model.EntityId == null) 
            return Task.CompletedTask;
        
        return _context
            .Database
            .ExecuteSqlRawAsync("EXECUTE dbo.UpdateAccountBalanceSummary @p0, @p1", 
                model.EntityId, 
                model.AirslipUserType);
    }
}