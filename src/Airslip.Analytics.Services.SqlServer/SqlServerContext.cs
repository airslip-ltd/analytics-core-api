using Airslip.Analytics.Core.Entities;
using Airslip.Common.Repository.Types.Entities;
using Airslip.Common.Services.SqlServer.Implementations;
using Microsoft.EntityFrameworkCore;

namespace Airslip.Analytics.Services.SqlServer;

public class SqlServerContext : AirslipSqlServerContextBase
{
    public SqlServerContext(DbContextOptions<SqlServerContext> options) 
        : base(options)
    {
        
    }
    
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<CountryCode> CountryCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Account>()
            .ToTable("Accounts")
            .HasKey(b => b.Id)
            .HasName("PK_Accounts_Id");
        
        modelBuilder
            .Entity<Bank>()
            .ToTable("Banks")
            .HasKey(b => b.Id)
            .HasName("PK_Banks_Id");
        
        modelBuilder
            .Entity<CountryCode>()
            .ToTable("CountryCodes")
            .HasKey(b => b.Id)
            .HasName("PK_CountryCodes_Id");

        modelBuilder
            .Entity<BasicAuditInformation>()
            .ToTable("AuditInformation")
            .HasKey(b => b.Id)
            .HasName("PK_AuditInformation_Id");
    }
}