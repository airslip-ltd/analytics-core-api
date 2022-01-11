using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Airslip.Analytics.Services.SqlServer;

public class SqlServerContextFactory : IDesignTimeDbContextFactory<SqlServerContext>
{
    public SqlServerContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<SqlServerContext> optionsBuilder = new();
        optionsBuilder.UseSqlServer("Server=tcp:gwdev-sqlserver.database.windows.net,1433;Initial Catalog=analytics;Persist Security Info=False;User ID=analytics_db_admin;Password=Secret1234.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        return new SqlServerContext(optionsBuilder.Options);
    }
}