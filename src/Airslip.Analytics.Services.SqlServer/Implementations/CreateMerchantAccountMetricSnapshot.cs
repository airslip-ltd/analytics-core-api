using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Airslip.Analytics.Services.SqlServer.Implementations;

public class CreateMerchantAccountMetricSnapshot : IAnalyticsProcess<MerchantTransactionModel>
{
    private readonly DbContext _context;

    public CreateMerchantAccountMetricSnapshot(IContext context)
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
            .ExecuteSqlRawAsync("EXEC dbo.CreateMerchantAccountMetricSnapshot @EntityId = {0}, @AirslipUserType = {1}, @Id = {2}",
                model.EntityId, 
                model.AirslipUserType,
                model.Id);
    }
}