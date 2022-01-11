using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Airslip.Analytics.Services.SqlServer;

public class SqlServerContextFactory : IDesignTimeDbContextFactory<SqlServerContext>
{
    public SqlServerContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<SqlServerContext> optionsBuilder = new();
        optionsBuilder.UseSqlServer("Server=(localdb);Integrated Security=true;");

        return new SqlServerContext(optionsBuilder.Options);
    }
}