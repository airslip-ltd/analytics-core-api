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
    
    public DbSet<MerchantCardDetail> MerchantCardDetails { get; set; }
    public DbSet<MerchantPaymentDetail> MerchantPaymentDetails { get; set; }
    public DbSet<MerchantDiscount> MerchantDiscounts { get; set; }
    public DbSet<MerchantProduct> MerchantProducts { get; set; }
    public DbSet<MerchantTransaction> MerchantTransactions { get; set; }
    public DbSet<MerchantVat> MerchantVat { get; set; }
    
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
        
        modelBuilder
            .Entity<MerchantCardDetail>()
            .ToTable("MerchantCardDetails")
            .HasKey(b => b.Id)
            .HasName("PK_MerchantCardDetails_Id");

        modelBuilder
            .Entity<MerchantPaymentDetail>()
            .ToTable("MerchantPaymentDetails")
            .HasKey(b => b.Id)
            .HasName("PK_MerchantPaymentDetails_Id");

        modelBuilder
            .Entity<MerchantDiscount>()
            .ToTable("MerchantDiscounts")
            .HasKey(b => b.Id)
            .HasName("PK_MerchantDiscounts_Id");
    
        modelBuilder
            .Entity<MerchantProduct>()
            .ToTable("MerchantProducts")
            .HasKey(b => b.Id)
            .HasName("PK_MerchantProducts_Id");

        modelBuilder
            .Entity<MerchantTransaction>()
            .ToTable("MerchantTransactions")
            .HasKey(b => b.Id)
            .HasName("PK_MerchantTransactions_Id");

        modelBuilder
            .Entity<MerchantVat>()
            .ToTable("MerchantVats")
            .HasKey(b => b.Id)
            .HasName("PK_MerchantVats_Id");

        // Defaults
        
        modelBuilder
            .Entity<BankAccountBalanceSummary>()
            .Property(b => b.Id)
            .HasDefaultValueSql("dbo.getId()");
        
        modelBuilder
            .Entity<BankBusinessBalance>()
            .Property(b => b.Id)
            .HasDefaultValueSql("dbo.getId()");
        
        
        // Precision
        
        modelBuilder.Entity<MerchantVat>()
            .Property(i => i.Rate)
            .HasColumnType("smallmoney");
        
        modelBuilder.Entity<MerchantProduct>()
            .Property(i => i.VatRate)
            .HasColumnType("smallmoney");
        
    }
}