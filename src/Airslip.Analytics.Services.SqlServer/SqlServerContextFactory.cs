using Airslip.Common.Repository.Configuration;
using Airslip.Common.Repository.Implementations;
using Airslip.Common.Services.SqlServer.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using Serilog.Core;

namespace Airslip.Analytics.Services.SqlServer;

public class SqlServerContextFactory : IDesignTimeDbContextFactory<SqlServerContext>
{
    public SqlServerContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<SqlServerContext> optionsBuilder = new();
        optionsBuilder.UseSqlServer("Server=(localdb);Integrated Security=true;");

        SqlServerContext context = new(optionsBuilder.Options,
            new RepositoryMetricService(Logger.None, new OptionsWrapper<RepositorySettings>(new RepositorySettings())), new QueryBuilder());
        
        return context;
    }
}