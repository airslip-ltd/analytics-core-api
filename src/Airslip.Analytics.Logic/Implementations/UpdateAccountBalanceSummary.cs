using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Airslip.Analytics.Logic.Implementations;

public class UpdateAccountBalanceSummary : IAnalyticsProcess<BalanceAnalysisModel>
{
    private readonly DbContext _context;

    public UpdateAccountBalanceSummary(IContext context)
    {
        if (context is not DbContext dbContext) 
            throw new ArgumentException("Invalid context", nameof(context));
        _context = dbContext;
    }
    
    public Task<int> Execute(BalanceAnalysisModel model)
    {
        return _context
            .Database
            .ExecuteSqlRawAsync("EXEC dbo.UpdateAccountBalanceSummary @EntityId = {0}, @AirslipUserType = {1}", 
                model.EntityId, 
                model.AirslipUserType);
    }
}