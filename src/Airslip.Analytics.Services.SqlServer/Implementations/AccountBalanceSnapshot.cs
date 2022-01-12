using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Airslip.Analytics.Services.SqlServer.Implementations;

public class GenerateAccountBalanceSnapshot : IAnalyticsProcess<AccountBalanceModel>
{
    private readonly DbContext _context;

    public GenerateAccountBalanceSnapshot(IContext context)
    {
        _context = context as DbContext;
    }
    
    public Task Execute(AccountBalanceModel model)
    {
        if (model.EntityId == null || model.Id == null) 
            return Task.CompletedTask;
        
        return _context
            .Database
            .ExecuteSqlRawAsync("EXECUTE dbo.CreateAccountBalanceSnapshot @p0, @p1, @p2", 
                model.EntityId, 
                model.AirslipUserType, 
                model.Id);
    }
}