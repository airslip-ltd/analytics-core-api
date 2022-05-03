using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Airslip.Analytics.Logic.Implementations;

public class CreateMerchantMetricSnapshot : IAnalyticsProcess<MerchantTransactionModel>
{
    private readonly DbContext _context;

    public CreateMerchantMetricSnapshot(IContext context)
    {
        if (context is not DbContext dbContext) 
            throw new ArgumentException("Invalid context", nameof(context));
        _context = dbContext;
    }
    
    public Task<int> Execute(MerchantTransactionModel model)
    {
        if (model.EntityId == null || model.Id == null) 
            return Task.FromResult(0);
        
        return _context
            .Database
            .ExecuteSqlRawAsync("EXEC dbo.CreateMerchantMetricSnapshot @Id = {0}",
                model.Id);
    }
}