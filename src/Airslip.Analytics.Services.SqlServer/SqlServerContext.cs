using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Entities.Unmapped;
using Airslip.Analytics.Services.SqlServer.Data;
using Airslip.Analytics.Services.SqlServer.Extensions;
using Airslip.Common.Repository.Types.Entities;
using Airslip.Common.Repository.Types.Enums;
using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Services.Excel.Implementations;
using Airslip.Common.Services.Excel.Interfaces;
using Airslip.Common.Services.SqlServer.Implementations;
using Airslip.Common.Services.SqlServer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Airslip.Analytics.Services.SqlServer;

public class SqlServerContext : AirslipSqlServerContextBase
{
    public SqlServerContext(DbContextOptions<SqlServerContext> options, 
        IRepositoryMetricService metricService, IQueryBuilder queryBuilder) 
        : base(options, metricService, queryBuilder)
    {
        
    }

    public DbSet<MerchantAccountMetricSnapshot> MerchantAccountMetricSnapshots { get; set; } = null!;
    public DbSet<MerchantRefund> MerchantRefunds { get; set; } = null!;
    public DbSet<MerchantRefundItem> MerchantRefundItems { get; set; } = null!;
    public DbSet<MerchantProduct> MerchantProducts { get; set; } = null!;
    public DbSet<MerchantTransaction> MerchantTransactions { get; set; } = null!;
    public DbSet<MerchantMetricSnapshot> MerchantMetricSnapshots { get; set; } = null!;
    public DbSet<BankAccountBalance> BankAccountBalances { get; set; } = null!;
    public DbSet<BankAccountBalanceSnapshot> BankAccountBalanceSnapshots { get; set; } = null!;
    public DbSet<BankAccountBalanceSummary> BankAccountBalanceSummary { get; set; } = null!;
    public DbSet<BankBusinessBalance> BankBusinessBalances { get; set; } = null!;
    public DbSet<BankBusinessBalanceSnapshot> BankBusinessBalanceSnapshots { get; set; } = null!;
    public DbSet<BankSyncRequest> BankSyncRequests { get; set; } = null!;
    public DbSet<BankTransaction> BankTransactions { get; set; } = null!;
    public DbSet<CountryCode> CountryCodes { get; set; } = null!;
    public DbSet<CurrencyDetail> CurrencyDetails { get; set; } = null!;
    
    public DbSet<BankAccountMetricSnapshot> BankAccountMetricSnapshots { get; set; } = null!;
    public DbSet<BasicAuditInformation> AuditInformation { get; set; } = null!;
    public DbSet<Integration> Integrations { get; set; } = null!;
    public DbSet<IntegrationAccountDetail> IntegrationAccountDetails { get; set; } = null!;
    public DbSet<RelationshipDetail> RelationshipDetails { get; set; } = null!;
    public DbSet<RelationshipHeader> RelationshipHeaders { get; set; } = null!;
    public DbSet<IntegrationProvider> IntegrationProviders { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<DashboardMetricSnapshot>().HasNoKey().ToView(null);
        modelBuilder.Entity<RevenueAndRefundsByYear>().HasNoKey().ToView(null);
        modelBuilder.Entity<DebitsAndCreditsByYear>().HasNoKey().ToView(null);
        modelBuilder.Entity<Currency>().HasNoKey().ToView(null);
        
        // Table names
        modelBuilder.AddTableWithDefaults<Integration>();
        modelBuilder.AddTableWithDefaults<BankAccountBalanceSnapshot>();
        modelBuilder.AddTableWithDefaults<BankAccountBalanceSummary>();
        modelBuilder.AddTableWithDefaults<BankAccountBalance>();
        modelBuilder.AddTableWithDefaults<IntegrationProvider>();
        
        modelBuilder.AddTableWithDefaults<BankBusinessBalance>();
        modelBuilder.AddTableWithDefaults<BankBusinessBalanceSnapshot>();
        modelBuilder.AddTableWithDefaults<BankTransaction>();
        
        modelBuilder.AddTableWithDefaults<BankSyncRequest>();
        modelBuilder.AddTableWithDefaults<CountryCode>();
        modelBuilder.AddTableWithDefaults<CurrencyDetail>();

        modelBuilder.AddTableWithDefaults<BasicAuditInformation>("AuditInformation");
        modelBuilder.AddTableWithDefaults<MerchantRefund>();
        modelBuilder.AddTableWithDefaults<MerchantRefundItem>();
        modelBuilder.AddTableWithDefaults<MerchantProduct>();
        modelBuilder.AddTableWithDefaults<MerchantTransaction>();
        modelBuilder.AddTableWithDefaults<MerchantMetricSnapshot>();
        modelBuilder.AddTableWithDefaults<BankAccountMetricSnapshot>();
        modelBuilder.AddTableWithDefaults<MerchantAccountMetricSnapshot>();
        
        modelBuilder.AddTableWithDefaults<RelationshipHeader>();
        modelBuilder.AddTableWithDefaults<RelationshipDetail>();
        
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
        modelBuilder.Entity<Integration>().Property(o => o.IntegrationProviderId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<BasicAuditInformation>().Property(o => o.Id).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<BasicAuditInformation>().Property(o => o.CreatedByUserId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<BasicAuditInformation>().Property(o => o.DeletedByUserId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<BasicAuditInformation>().Property(o => o.UpdatedByUserId).HasColumnType(Constants.ID_DATA_TYPE);

        modelBuilder.Entity<BankAccountBalanceSnapshot>().Property(o => o.EntityId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<BankAccountBalanceSnapshot>().Property(o => o.IntegrationId).HasColumnType(Constants.ID_DATA_TYPE);
        
        modelBuilder.Entity<BankSyncRequest>().Property(o => o.IntegrationId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<BankAccountBalance>().Property(o => o.IntegrationId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<BankAccountBalanceSummary>().Property(o => o.IntegrationId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<BankAccountMetricSnapshot>().Property(o => o.IntegrationId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<BankSyncRequest>().Property(o => o.IntegrationId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<BankTransaction>().Property(o => o.IntegrationId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<MerchantAccountMetricSnapshot>().Property(o => o.IntegrationId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.IntegrationId).HasColumnType(Constants.ID_DATA_TYPE);

        modelBuilder.Entity<BankSyncRequest>().Property(o => o.FromDate).HasColumnType("nvarchar (50)");
        modelBuilder.Entity<BankSyncRequest>().Property(o => o.ApplicationUserId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<BankSyncRequest>().Property(o => o.LastCardDigits).HasColumnType("nvarchar (20)");
        modelBuilder.Entity<BankSyncRequest>().Property(o => o.TracingId).HasColumnType(Constants.ID_DATA_TYPE);
        
        modelBuilder.Entity<BankTransaction>().Property(o => o.BankTransactionId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<BankTransaction>().Property(o => o.TransactionHash).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<BankTransaction>().Property(o => o.IntegrationProviderId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<BankTransaction>().Property(o => o.EmailAddress).HasColumnType("nvarchar (100)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.Description).HasColumnType("nvarchar (150)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.AddressLine).HasColumnType("nvarchar (50)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.LastCardDigits).HasColumnType("nvarchar (20)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.IsoFamilyCode).HasColumnType("nvarchar (50)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.ProprietaryCode).HasColumnType("nvarchar (50)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.TransactionIdentifier).HasColumnType("nvarchar (50)");
        modelBuilder.Entity<BankTransaction>().Property(o => o.Reference).HasColumnType("nvarchar (50)");
        
        modelBuilder.Entity<Integration>().Property(o => o.Name).HasColumnType("nvarchar (50)");
        modelBuilder.Entity<Integration>().Property(o => o.AccountDetailId).HasColumnType(Constants.ID_DATA_TYPE);

        modelBuilder.Entity<MerchantTransaction>().Property(o => o.TrackingId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.InternalId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.Source).HasColumnType("nvarchar (50)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.TransactionNumber).HasColumnType("nvarchar (50)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.RefundCode).HasColumnType("nvarchar (50)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.BankStatementDescription).HasColumnType("nvarchar (150)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.BankStatementTransactionIdentifier).HasColumnType("nvarchar (50)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.StoreLocationId).HasColumnType("nvarchar (50)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.StoreAddress).HasColumnType("nvarchar (250)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.CustomerEmail).HasColumnType("nvarchar (100)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.OperatorName).HasColumnType("nvarchar (100)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.Time).HasColumnType("nvarchar (10)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.Till).HasColumnType("nvarchar (10)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.Number).HasColumnType("nvarchar (50)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.Store).HasColumnType("nvarchar (50)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.Description).HasColumnType("nvarchar (150)");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.OrderStatus).HasColumnType("nvarchar (20)").HasDefaultValue("Unknown");
        modelBuilder.Entity<MerchantTransaction>().Property(o => o.PaymentStatus).HasColumnType("nvarchar (20)").HasDefaultValue("Unknown");
        
        modelBuilder.Entity<IntegrationProvider>().Property(o => o.FriendlyName).HasColumnType("nvarchar (150)");
        modelBuilder.Entity<IntegrationProvider>().Property(o => o.Integration).HasColumnType("nvarchar (50)").HasDefaultValue("hub");
        modelBuilder.Entity<IntegrationProvider>().Property(o => o.Provider).HasColumnType("nvarchar (50)");
        modelBuilder.Entity<IntegrationProvider>().Property(o => o.Name).HasColumnType("nvarchar (150)");
        
        modelBuilder.AddCurrencyCode<BankAccountBalance>();
        modelBuilder.AddCurrencyCode<BankAccountBalanceSnapshot>();
        modelBuilder.AddCurrencyCode<BankAccountBalanceSummary>();
        modelBuilder.AddCurrencyCode<BankBusinessBalanceSnapshot>();
        modelBuilder.AddCurrencyCode<BankBusinessBalance>();
        
        modelBuilder.AddCurrencyCode<MerchantAccountMetricSnapshot>();
        modelBuilder.AddCurrencyCode<BankAccountMetricSnapshot>();
        modelBuilder.AddCurrencyCode<MerchantMetricSnapshot>();
        modelBuilder.AddCurrencyCode<MerchantTransaction>();
        modelBuilder.AddCurrencyCode<BankTransaction>();
        
        modelBuilder.Entity<IntegrationAccountDetail>().Property(o => o.LastCardDigits).HasColumnType("nvarchar (20)");
        modelBuilder.Entity<IntegrationAccountDetail>().Property(o => o.CurrencyCode).IsCurrencyCode();
        modelBuilder.Entity<IntegrationAccountDetail>().Property(o => o.SortCode).HasColumnType("nvarchar (10)");
        modelBuilder.Entity<IntegrationAccountDetail>().Property(o => o.AccountNumber).HasColumnType("nvarchar (10)");
        modelBuilder.Entity<IntegrationAccountDetail>().Property(o => o.AccountId).HasColumnType("nvarchar(100)");

        modelBuilder.Entity<MerchantRefund>().Property(o => o.Comment).HasColumnType("nvarchar (250)");
        
        modelBuilder.Entity<MerchantRefundItem>().Property(o => o.ProductId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<MerchantRefundItem>().Property(o => o.VariantId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<MerchantRefundItem>().Property(o => o.TransactionProductId).HasColumnType(Constants.ID_DATA_TYPE);
        
        modelBuilder.Entity<MerchantRefundItem>().Property(o => o.VariantId).HasColumnType(Constants.ID_DATA_TYPE);
        
        modelBuilder.Entity<RelationshipDetail>().Property(o => o.PermissionType).HasColumnType("nvarchar (50)");
        modelBuilder.Entity<RelationshipDetail>().Property(o => o.OwnerEntityId).HasColumnType(Constants.ID_DATA_TYPE);
        modelBuilder.Entity<RelationshipDetail>().Property(o => o.ViewerEntityId).HasColumnType(Constants.ID_DATA_TYPE);
       
        // Relationships
        modelBuilder.Entity<Integration>()
            .HasOne(t => t.Provider)
            .WithMany(t => t.Integrations)
            .HasForeignKey(t => t.IntegrationProviderId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        modelBuilder.Entity<RelationshipHeader>()
            .HasMany(t => t.Details)
            .WithOne(t => t.RelationshipHeader)
            .HasForeignKey(x => x.RelationshipHeaderId)
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
        
        // Indexes
        modelBuilder.Entity<BankTransaction>()
            .HasIndex(b => new
            {
                b.AirslipUserType, b.EntityId, b.Day, b.Month, b.Year
            }).IncludeProperties( p => new
            {
                p.IntegrationId, p.Amount, p.IntegrationProviderId
            });
        
        modelBuilder.Entity<BankAccountMetricSnapshot>()
            .HasIndex(b => new
            {
                b.EntityId, b.AirslipUserType, b.Day, b.Month, b.Year, b.IntegrationId
            });
        
        modelBuilder.Entity<BankTransaction>()
            .HasIndex(b => new
            {
                b.EntityId, b.AirslipUserType
            }).IncludeProperties( p => new
            {
                p.IntegrationProviderId
            });
        
        modelBuilder.Entity<RelationshipDetail>()
            .HasIndex(b => new
            {
                b.OwnerEntityId, b.OwnerAirslipUserType, b.ViewerEntityId, b.ViewerAirslipUserType, b.PermissionType,
                b.Allowed
            });
        
        modelBuilder.Entity<BankAccountBalanceSnapshot>()
            .HasIndex(b => new
            {
                b.EntityId, b.AirslipUserType, b.IntegrationId, b.UpdatedOn, b.TimeStamp, b.AccountType, b.CurrencyCode
            }).IncludeProperties(p => new
            {
                p.Balance
            });
        
        modelBuilder.Entity<BankAccountBalanceSnapshot>()
            .HasIndex(b => new
            {
                b.EntityId, b.AirslipUserType, b.AccountType
            });
        
        modelBuilder.Entity<BankBusinessBalance>()
            .HasIndex(b => new
            {
                b.EntityId, b.AirslipUserType, b.AccountType, b.CurrencyCode
            });
        
        modelBuilder.Entity<MerchantTransaction>()
            .HasIndex(b => new
            {
                b.Day, b.Month, b.Year, b.EntityId, b.AirslipUserType
            });
        
        modelBuilder.Entity<MerchantTransaction>()
            .HasIndex(b => new
            {
                b.Day, b.Month, b.Year, b.EntityId, b.AirslipUserType, b.IntegrationId, b.CurrencyCode
            });
        
        modelBuilder.Entity<MerchantAccountMetricSnapshot>()
            .HasIndex(b => new
            {
                b.Day, b.Month, b.Year, b.EntityId, b.AirslipUserType, b.IntegrationId, b.CurrencyCode
            });
        
        // Default data
        // Init
        ISpreadsheetReader reader = new ExcelReader();
        using (Stream? fileStream = GetType().Assembly
                   .GetManifestResourceStream("Airslip.Analytics.Services.SqlServer.Data.country-codes.xlsx"))
        {
            if (fileStream == null) throw new FileNotFoundException("CountryCodes not found");

            // Add the country codes
            List<CountryCode> countryCodes = reader.ReadDataFromSheet<CountryCode>(fileStream, 1, 1);
            countryCodes.ForEach(o => o.EntityStatus = EntityStatus.Active);
            modelBuilder.Entity<CountryCode>().HasData(countryCodes);            
        }

        using (Stream? fileStream = GetType().Assembly
                   .GetManifestResourceStream("Airslip.Analytics.Services.SqlServer.Data.country-codes.xlsx"))
        {
            if (fileStream == null) throw new FileNotFoundException("CountryCodes not found");

            // Add the country codes
            List<CurrencyDetail> currencyDetails = reader.ReadDataFromSheet<CurrencyDetail>(fileStream, 2, 1);
            currencyDetails.ForEach(o => o.EntityStatus = EntityStatus.Active);
            modelBuilder.Entity<CurrencyDetail>().HasData(currencyDetails);         
        }
    }
}