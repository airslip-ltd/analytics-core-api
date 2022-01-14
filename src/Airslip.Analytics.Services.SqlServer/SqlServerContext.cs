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
    public DbSet<AccountBalance> AccountBalances { get; set; }
    public DbSet<AccountBalanceSnapshot> AccountBalanceSnapshots { get; set; }
    public DbSet<AccountBalanceSummary> AccountBalanceSummary { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<BusinessBalance> BusinessBalances { get; set; }
    public DbSet<BusinessBalanceSnapshot> BusinessBalanceSnapshots { get; set; }
    public DbSet<SyncRequest> SyncRequests { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<CountryCode> CountryCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Table names
        modelBuilder
            .Entity<AccountBalanceSnapshot>()
            .ToTable("AccountBalanceSnapshots")
            .HasKey(b => b.Id)
            .HasName("PK_AccountBalanceSnapshots_Id");
        
        modelBuilder
            .Entity<AccountBalanceSummary>()
            .ToTable("AccountBalanceSummaries")
            .HasKey(b => b.Id)
            .HasName("PK_AccountBalanceSummaries_Id");
        
        modelBuilder
            .Entity<AccountBalance>()
            .ToTable("AccountBalances")
            .HasKey(b => b.Id)
            .HasName("PK_AccountBalances_Id");
        
        modelBuilder
            .Entity<AccountBalanceDetail>()
            .ToTable("AccountBalanceDetails")
            .HasKey(b => b.Id)
            .HasName("PK_AccountBalanceDetails_Id");
        
        modelBuilder
            .Entity<AccountBalanceCreditLine>()
            .ToTable("AccountBalanceCreditLines")
            .HasKey(b => b.Id)
            .HasName("PK_AccountBalanceCreditLines_Id");
        
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
            .Entity<BusinessBalance>()
            .ToTable("BusinessBalances")
            .HasKey(b => b.Id)
            .HasName("PK_BusinessBalances_Id");
        
        modelBuilder
            .Entity<BusinessBalanceSnapshot>()
            .ToTable("BusinessBalanceSnapshots")
            .HasKey(b => b.Id)
            .HasName("PK_BusinessBalanceSnapshots_Id");
        
        modelBuilder
            .Entity<Transaction>()
            .ToTable("Transactions")
            .HasKey(b => b.Id)
            .HasName("PK_Transactions_Id");
        
        modelBuilder
            .Entity<SyncRequest>()
            .ToTable("SyncRequests")
            .HasKey(b => b.Id)
            .HasName("PK_SyncRequests_Id");
        
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
        
        // Defaults
        
        modelBuilder
            .Entity<AccountBalanceSummary>()
            .Property(b => b.Id)
            .HasDefaultValueSql("dbo.getId()");
        
        modelBuilder
            .Entity<BusinessBalance>()
            .Property(b => b.Id)
            .HasDefaultValueSql("dbo.getId()");
    }
}