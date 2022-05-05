using Airslip.Analytics.Core.Entities.Unmapped;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Services.SqlServer;
using Airslip.Common.Auth.Interfaces;
using Airslip.Common.Auth.Models;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Types.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Airslip.Analytics.Reports.Implementations;

public class DataListService : IDataListService
{
    private readonly UserToken _userToken;
    private readonly SqlServerContext _context;

    public DataListService(IContext context, ITokenDecodeService<UserToken> userToken)
    {
        if (context is not SqlServerContext dbContext) 
            throw new ArgumentException("Invalid context", nameof(context));
        _userToken = userToken.GetCurrentToken();
        _context = dbContext;
    }
    
    public async Task<IResponse> GetCurrencies(DataSearchModel query)
    {
        IQueryable<Currency> q = _context
            .Set<Currency>()
            .FromSqlRaw(@$"dbo.GetCurrencies @ViewerEntityId = {{0}}, 
@ViewerAirslipUserType = {{1}}, @OwnerEntityId = {{2}}, @OwnerAirslipUserType = {{3}}",
                _userToken.EntityId,
                _userToken.AirslipUserType,
                query.OwnerEntityId,
                query.OwnerAirslipUserType);

        List<Currency> metrics = await q.ToListAsync();

        return new DataSearchResponse<Currency>(metrics);
    }
}