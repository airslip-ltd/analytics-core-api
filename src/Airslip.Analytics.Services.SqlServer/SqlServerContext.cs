using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Entities.Unmapped;
using Airslip.Analytics.Services.SqlServer.Extensions;
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
    
    public DbSet<MerchantAccountMetricSnapshot> MerchantAccountMetricSnapshots { get; set; }
    public DbSet<MerchantRefund> MerchantRefunds { get; set; }
    public DbSet<MerchantRefundItem> MerchantRefundItems { get; set; }
    public DbSet<MerchantProduct> MerchantProducts { get; set; }
    public DbSet<MerchantTransaction> MerchantTransactions { get; set; }
    public DbSet<MerchantMetricSnapshot> MerchantMetricSnapshots { get; set; }
    public DbSet<BankAccountBalance> BankAccountBalances { get; set; }
    public DbSet<BankAccountBalanceSnapshot> BankAccountBalanceSnapshots { get; set; }
    public DbSet<BankAccountBalanceSummary> BankAccountBalanceSummary { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<BankBusinessBalance> BankBusinessBalances { get; set; }
    public DbSet<BankBusinessBalanceSnapshot> BankBusinessBalanceSnapshots { get; set; }
    public DbSet<BankSyncRequest> BankSyncRequests { get; set; }
    public DbSet<BankTransaction> BankTransactions { get; set; }
    public DbSet<CountryCode> CountryCodes { get; set; }
    public DbSet<BankAccountMetricSnapshot> BankAccountMetricSnapshots { get; set; }
    public DbSet<BasicAuditInformation> AuditInformation { get; set; } = null!;
    public DbSet<Integration> Integrations { get; set; } = null!;
    public DbSet<IntegrationAccountDetail> IntegrationAccountDetails { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<DashboardMetricSnapshot>().HasNoKey().ToView(null);
        modelBuilder.Entity<RevenueAndRefundsByYear>().HasNoKey().ToView(null);
        modelBuilder.Entity<DebitsAndCreditsByYear>().HasNoKey().ToView(null);
        
        // Table names
        modelBuilder.AddTableWithDefaults<Integration>();
        modelBuilder.AddTableWithDefaults<BankAccountBalanceSnapshot>();
        modelBuilder.AddTableWithDefaults<BankAccountBalanceSummary>();
        modelBuilder.AddTableWithDefaults<BankAccountBalance>();
        modelBuilder.AddTableWithDefaults<BankAccountBalanceDetail>();
        modelBuilder.AddTableWithDefaults<BankAccountBalanceCreditLine>();
        
        modelBuilder.AddTableWithDefaults<Bank>();
        modelBuilder.AddTableWithDefaults<BankBusinessBalance>();
        modelBuilder.AddTableWithDefaults<BankBusinessBalanceSnapshot>();
        modelBuilder.AddTableWithDefaults<BankTransaction>();
        
        modelBuilder.AddTableWithDefaults<BankSyncRequest>();
        modelBuilder.AddTableWithDefaults<CountryCode>();

        modelBuilder.AddTableWithDefaults<BasicAuditInformation>("AuditInformation");
        
        
        modelBuilder.AddTableWithDefaults<MerchantRefund>();
        modelBuilder.AddTableWithDefaults<MerchantRefundItem>();
        modelBuilder.AddTableWithDefaults<MerchantProduct>();
        
        
        modelBuilder.AddTableWithDefaults<MerchantTransaction>();
        modelBuilder.AddTableWithDefaults<MerchantMetricSnapshot>();
        modelBuilder.AddTableWithDefaults<BankAccountMetricSnapshot>();
        modelBuilder.AddTableWithDefaults<MerchantAccountMetricSnapshot>();
        
        // Defaults
        modelBuilder.AddDatabaseGeneratedId<BankAccountBalanceSummary>();

        modelBuilder.AddDatabaseGeneratedId<BankBusinessBalance>();
        modelBuilder.AddDatabaseGeneratedId<MerchantMetricSnapshot>();
        modelBuilder.AddDatabaseGeneratedId<BankAccountMetricSnapshot>();
        modelBuilder.AddDatabaseGeneratedId<MerchantAccountMetricSnapshot>();
        
        // Types
        
        modelBuilder
            .Entity<MerchantAccountMetricSnapshot>()
            .Property(b => b.MetricDate)
            .HasColumnType("date");
        
        modelBuilder
            .Entity<MerchantMetricSnapshot>()
            .Property(b => b.MetricDate)
            .HasColumnType("date");
        
        modelBuilder
            .Entity<BankAccountMetricSnapshot>()
            .Property(b => b.MetricDate)
            .HasColumnType("date");
        
        // Data types
        modelBuilder.Entity<Integration>().Property(o => o.IntegrationProviderId).HasColumnType("varchar (50)");
        modelBuilder.Entity<BasicAuditInformation>().Property(o => o.Id).HasColumnType("varchar (50)");
        modelBuilder.Entity<BasicAuditInformation>().Property(o => o.CreatedByUserId).HasColumnType("varchar (50)");
        modelBuilder.Entity<BasicAuditInformation>().Property(o => o.DeletedByUserId).HasColumnType("varchar (50)");
        modelBuilder.Entity<BasicAuditInformation>().Property(o => o.UpdatedByUserId).HasColumnType("varchar (50)");

        modelBuilder.Entity<BankAccountBalanceSnapshot>().Property(o => o.EntityId).HasColumnType("varchar (50)");
        modelBuilder.Entity<BankAccountBalanceSnapshot>().Property(o => o.AccountId).HasColumnType("varchar (50)");
        modelBuilder.Entity<BankAccountBalanceSnapshot>().Property(o => o.AccountId).HasColumnType("varchar (5)");
        
        modelBuilder.Entity<Bank>().Property(o => o.TradingName).HasColumnType("varchar (50)");
        modelBuilder.Entity<Bank>().Property(o => o.AccountName).HasColumnType("varchar (50)");
        
        modelBuilder.Entity<BankSyncRequest>().Property(o => o.FromDate).HasColumnType("varchar (50)");
        modelBuilder.Entity<BankSyncRequest>().Property(o => o.ApplicationUserId).HasColumnType("varchar (50)");
        modelBuilder.Entity<BankSyncRequest>().Property(o => o.LastCardDigits).HasColumnType("varchar (20)");
        modelBuilder.Entity<BankSyncRequest>().Property(o => o.TracingId).HasColumnType("varchar (50)");
        
        modelBuilder.Entity<BankTransaction>().Property(o => o.BankTransactionId).HasColumnType("varchar (50)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.TransactionHash).HasColumnType("varchar (50)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.BankId).HasColumnType("varchar (50)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.EmailAddress).HasColumnType("varchar (100)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.CurrencyCode).HasColumnType("varchar (5)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.Description).HasColumnType("varchar (150)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.AddressLine).HasColumnType("varchar (50)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.LastCardDigits).HasColumnType("varchar (20)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.IsoFamilyCode).HasColumnType("varchar (50)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.ProprietaryCode).HasColumnType("varchar (50)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.TransactionIdentifier).HasColumnType("varchar (50)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.Reference).HasColumnType("varchar (50)");
        
        modelBuilder.Entity<Integration>().Property(o => o.Name).HasColumnType("varchar (50)");
        modelBuilder.Entity<Integration>().Property(o => o.AccountDetailId).HasColumnType("varchar (50)");

        modelBuilder.Entity<MerchantTransaction>().Property(o => o.TrackingId).HasColumnType("varchar (50)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.InternalId).HasColumnType("varchar (50)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.Source).HasColumnType("varchar (50)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.TransactionNumber).HasColumnType("varchar (50)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.RefundCode).HasColumnType("varchar (50)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.BankStatementDescription).HasColumnType("varchar (150)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.BankStatementTransactionIdentifier).HasColumnType("varchar (50)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.StoreLocationId).HasColumnType("varchar (50)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.StoreAddress).HasColumnType("varchar (250)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.CurrencyCode).HasColumnType("varchar (5)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.CustomerEmail).HasColumnType("varchar (100)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.OperatorName).HasColumnType("varchar (100)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.Time).HasColumnType("varchar (10)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.Till).HasColumnType("varchar (10)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.Number).HasColumnType("varchar (50)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.Store).HasColumnType("varchar (50)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.Description).HasColumnType("varchar (150)");
        
        modelBuilder.Entity<BankAccountBalanceDetail>().Property(o => o.AccountBalanceId).HasColumnType("varchar (50)");
        modelBuilder.Entity<BankAccountBalanceDetail>().Property(o => o.DateTime).HasColumnType("varchar (50)");
        modelBuilder.Entity<BankAccountBalanceDetail>().Property(o => o.Currency).HasColumnType("varchar (5)");
        
        modelBuilder.Entity<IntegrationAccountDetail>().Property(o => o.LastCardDigits).HasColumnType("varchar (20)");
        modelBuilder.Entity<IntegrationAccountDetail>().Property(o => o.CurrencyCode).HasColumnType("varchar (5)");
        modelBuilder.Entity<IntegrationAccountDetail>().Property(o => o.UsageType).HasColumnType("varchar (50)");
        modelBuilder.Entity<IntegrationAccountDetail>().Property(o => o.AccountType).HasColumnType("varchar (50)");
        modelBuilder.Entity<IntegrationAccountDetail>().Property(o => o.SortCode).HasColumnType("varchar (10)");
        modelBuilder.Entity<IntegrationAccountDetail>().Property(o => o.AccountNumber).HasColumnType("varchar (10)");
        modelBuilder.Entity<IntegrationAccountDetail>().Property(o => o.AccountId).HasColumnType("varchar (50)");

        modelBuilder.Entity<MerchantRefund>().Property(o => o.Comment).HasColumnType("varchar (250)");
        
        modelBuilder.Entity<BankAccountBalanceCreditLine>().Property(o => o.Currency).HasColumnType("varchar (5)");
        modelBuilder.Entity<BankAccountBalanceCreditLine>().Property(o => o.AccountBalanceDetailId).HasColumnType("varchar (50)");
        
        modelBuilder.Entity<MerchantRefundItem>().Property(o => o.ProductId).HasColumnType("varchar (50)");
        modelBuilder.Entity<MerchantRefundItem>().Property(o => o.VariantId).HasColumnType("varchar (50)");
        modelBuilder.Entity<MerchantRefundItem>().Property(o => o.TransactionProductId).HasColumnType("varchar (50)");
        
        modelBuilder.Entity<MerchantRefundItem>().Property(o => o.VariantId).HasColumnType("varchar (50)");
        
        // Custom keys
        modelBuilder.Entity<BankCountryCode>().HasKey(e => new
        {
            e.Id, e.BankId
        });
        
        // Relationships
        modelBuilder.Entity<Bank>()
            .HasMany(t => t.CountryCodes)
            .WithOne(t => t.Bank)
            .HasForeignKey(x => x.BankId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        modelBuilder.Entity<BankTransaction>()
            .HasOne(t => t.AuditInformation)
            .WithOne();

        modelBuilder.Entity<Integration>()
            .HasOne(t => t.AccountDetail)
            .WithOne(t => t.Integration)
            .HasForeignKey<IntegrationAccountDetail>(t => t.IntegrationId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
    }
}