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
    
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<BankAccountBalance> BankAccountBalances { get; set; }
    public DbSet<BankAccountBalanceSnapshot> BankAccountBalanceSnapshots { get; set; }
    public DbSet<BankAccountBalanceSummary> BankAccountBalanceSummary { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<BankBusinessBalance> BankBusinessBalances { get; set; }
    public DbSet<BankBusinessBalanceSnapshot> BankBusinessBalanceSnapshots { get; set; }
    public DbSet<BankSyncRequest> BankSyncRequests { get; set; }
    public DbSet<BankTransaction> BankTransactions { get; set; }
    public DbSet<CountryCode> CountryCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Table names
        modelBuilder
            .Entity<BankAccountBalanceSnapshot>()
            .ToTable("BankAccountBalanceSnapshots")
            .HasKey(b => b.Id)
            .HasName("PK_BankAccountBalanceSnapshots_Id");
        
        modelBuilder
            .Entity<BankAccountBalanceSummary>()
            .ToTable("BankAccountBalanceSummaries")
            .HasKey(b => b.Id)
            .HasName("PK_BankAccountBalanceSummaries_Id");
        
        modelBuilder
            .Entity<BankAccountBalance>()
            .ToTable("BankAccountBalances")
            .HasKey(b => b.Id)
            .HasName("PK_BankAccountBalances_Id");
        
        modelBuilder
            .Entity<BankAccountBalanceDetail>()
            .ToTable("BankAccountBalanceDetails")
            .HasKey(b => b.Id)
            .HasName("PK_BankAccountBalanceDetails_Id");
        
        modelBuilder
            .Entity<BankAccountBalanceCreditLine>()
            .ToTable("BankAccountBalanceCreditLines")
            .HasKey(b => b.Id)
            .HasName("PK_BankAccountBalanceCreditLines_Id");
        
        modelBuilder
            .Entity<BankAccount>()
            .ToTable("BankAccounts")
            .HasKey(b => b.Id)
            .HasName("PK_BankAccounts_Id");
        
        modelBuilder
            .Entity<Bank>()
            .ToTable("Banks")
            .HasKey(b => b.Id)
            .HasName("PK_Banks_Id");
        
        modelBuilder
            .Entity<BankBusinessBalance>()
            .ToTable("BankBusinessBalances")
            .HasKey(b => b.Id)
            .HasName("PK_BankBusinessBalances_Id");
        
        modelBuilder
            .Entity<BankBusinessBalanceSnapshot>()
            .ToTable("BankBusinessBalanceSnapshots")
            .HasKey(b => b.Id)
            .HasName("PK_BankBusinessBalanceSnapshots_Id");
        
        modelBuilder
            .Entity<BankTransaction>()
            .ToTable("BankTransactions")
            .HasKey(b => b.Id)
            .HasName("PK_BankTransactions_Id");
        
        modelBuilder
            .Entity<BankSyncRequest>()
            .ToTable("BankSyncRequests")
            .HasKey(b => b.Id)
            .HasName("PK_BankSyncRequests_Id");
        
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
            .Entity<BankAccountBalanceSummary>()
            .Property(b => b.Id)
            .HasDefaultValueSql("dbo.getId()");
        
        modelBuilder
            .Entity<BankBusinessBalance>()
            .Property(b => b.Id)
            .HasDefaultValueSql("dbo.getId()");
    }
}