using Airslip.Analytics.Services.SqlServer.Extensions;
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditInformation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditInformation_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountBalanceSnapshots",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    IntegrationId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "varchar (5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountBalanceSnapshots_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountBalanceSummaries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false, defaultValueSql: "dbo.getId()"),
                    IntegrationId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    Movement = table.Column<double>(type: "float", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "varchar (5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountBalanceSummaries_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountMetricSnapshots",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false, defaultValueSql: "dbo.getId()"),
                    IntegrationId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    MetricDate = table.Column<DateTime>(type: "date", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Day = table.Column<int>(type: "int", nullable: true),
                    TotalTransaction = table.Column<long>(type: "bigint", nullable: false),
                    TransactionCount = table.Column<int>(type: "int", nullable: false),
                    TotalCredit = table.Column<long>(type: "bigint", nullable: false),
                    CreditCount = table.Column<int>(type: "int", nullable: false),
                    TotalDebit = table.Column<long>(type: "bigint", nullable: false),
                    DebitCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountMetricSnapshots_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankBusinessBalances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false, defaultValueSql: "dbo.getId()"),
                    EntityId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    Movement = table.Column<double>(type: "float", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "varchar (5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankBusinessBalances_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankBusinessBalanceSnapshots",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "varchar (5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankBusinessBalanceSnapshots_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryCodes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCodes_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MerchantAccountMetricSnapshots",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false, defaultValueSql: "dbo.getId()"),
                    IntegrationId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    MetricDate = table.Column<DateTime>(type: "date", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Day = table.Column<int>(type: "int", nullable: true),
                    OrderCount = table.Column<int>(type: "int", nullable: false),
                    TotalSales = table.Column<long>(type: "bigint", nullable: false),
                    SaleCount = table.Column<int>(type: "int", nullable: false),
                    TotalRefunds = table.Column<long>(type: "bigint", nullable: false),
                    RefundCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantAccountMetricSnapshots_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MerchantMetricSnapshots",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false, defaultValueSql: "dbo.getId()"),
                    EntityId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    MetricDate = table.Column<DateTime>(type: "date", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Day = table.Column<int>(type: "int", nullable: true),
                    OrderCount = table.Column<int>(type: "int", nullable: false),
                    TotalSales = table.Column<long>(type: "bigint", nullable: false),
                    SaleCount = table.Column<int>(type: "int", nullable: false),
                    TotalRefunds = table.Column<long>(type: "bigint", nullable: false),
                    RefundCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantMetricSnapshots_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountBalances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    IntegrationId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    BalanceStatus = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "varchar (5)", nullable: true),
                    DataSource = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountBalances_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccountBalances_AuditInformation_AuditInformationId",
                        column: x => x.AuditInformationId,
                        principalTable: "AuditInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    TradingName = table.Column<string>(type: "nvarchar (50)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar (50)", nullable: false),
                    EnvironmentType = table.Column<int>(type: "int", nullable: false),
                    DataSource = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banks_AuditInformation_AuditInformationId",
                        column: x => x.AuditInformationId,
                        principalTable: "AuditInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankSyncRequests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    IntegrationId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    UsageType = table.Column<int>(type: "int", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    LastCardDigits = table.Column<string>(type: "nvarchar (20)", nullable: true),
                    FromDate = table.Column<string>(type: "nvarchar (50)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    SyncStatus = table.Column<int>(type: "int", nullable: false),
                    RecordCount = table.Column<int>(type: "int", nullable: false),
                    TracingId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    DataSource = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankSyncRequests_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankSyncRequests_AuditInformation_AuditInformationId",
                        column: x => x.AuditInformationId,
                        principalTable: "AuditInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankTransactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    IntegrationId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    BankTransactionId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TransactionHash = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    BankId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar (100)", nullable: false),
                    AuthorisedDate = table.Column<long>(type: "bigint", nullable: true),
                    CapturedDate = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar (5)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar (150)", nullable: false),
                    AddressLine = table.Column<string>(type: "nvarchar (50)", nullable: true),
                    LastCardDigits = table.Column<string>(type: "nvarchar (20)", nullable: true),
                    IsoFamilyCode = table.Column<string>(type: "nvarchar (50)", nullable: true),
                    ProprietaryCode = table.Column<string>(type: "nvarchar (50)", nullable: true),
                    TransactionIdentifier = table.Column<string>(type: "nvarchar (50)", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar (50)", nullable: true),
                    DataSource = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Day = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTransactions_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankTransactions_AuditInformation_AuditInformationId",
                        column: x => x.AuditInformationId,
                        principalTable: "AuditInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Integrations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    IntegrationProviderId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IntegrationType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar (50)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    AuthenticationState = table.Column<int>(type: "int", nullable: false),
                    DataSource = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    AccountDetailId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integrations_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Integrations_AuditInformation_AuditInformationId",
                        column: x => x.AuditInformationId,
                        principalTable: "AuditInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MerchantTransactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    IntegrationId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    DataSource = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    TrackingId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    InternalId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar (50)", nullable: true),
                    TransactionNumber = table.Column<string>(type: "nvarchar (50)", nullable: true),
                    RefundCode = table.Column<string>(type: "nvarchar (50)", nullable: true),
                    Datetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BankStatementDescription = table.Column<string>(type: "nvarchar (150)", nullable: true),
                    BankStatementTransactionIdentifier = table.Column<string>(type: "nvarchar (50)", nullable: true),
                    StoreLocationId = table.Column<string>(type: "nvarchar (50)", nullable: true),
                    StoreAddress = table.Column<string>(type: "nvarchar (250)", nullable: true),
                    OnlinePurchase = table.Column<bool>(type: "bit", nullable: true),
                    Subtotal = table.Column<long>(type: "bigint", nullable: true),
                    ServiceCharge = table.Column<long>(type: "bigint", nullable: true),
                    Total = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar (5)", nullable: true),
                    CustomerEmail = table.Column<string>(type: "nvarchar (100)", nullable: true),
                    OperatorName = table.Column<string>(type: "nvarchar (100)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Time = table.Column<string>(type: "nvarchar (10)", nullable: true),
                    Till = table.Column<string>(type: "nvarchar (10)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar (50)", nullable: true),
                    Store = table.Column<string>(type: "nvarchar (50)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar (150)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Day = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantTransactions_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MerchantTransactions_AuditInformation_AuditInformationId",
                        column: x => x.AuditInformationId,
                        principalTable: "AuditInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RelationshipHeaders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    DataSource = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipHeaders_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelationshipHeaders_AuditInformation_AuditInformationId",
                        column: x => x.AuditInformationId,
                        principalTable: "AuditInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankAccountBalanceDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AccountBalanceId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    BalanceType = table.Column<int>(type: "int", nullable: false),
                    CreditLineIncluded = table.Column<bool>(type: "bit", nullable: false),
                    DateTime = table.Column<string>(type: "nvarchar (50)", nullable: true),
                    BalanceStatus = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar (5)", nullable: true),
                    BankAccountBalanceId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountBalanceDetails_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccountBalanceDetails_BankAccountBalances_BankAccountBalanceId",
                        column: x => x.BankAccountBalanceId,
                        principalTable: "BankAccountBalances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankCountryCode",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BankId = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCountryCode", x => new { x.Id, x.BankId });
                    table.ForeignKey(
                        name: "FK_BankCountryCode_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationAccountDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountStatus = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastCardDigits = table.Column<string>(type: "nvarchar (20)", nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar (5)", nullable: false),
                    UsageType = table.Column<string>(type: "nvarchar (50)", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar (50)", nullable: false),
                    SortCode = table.Column<string>(type: "nvarchar (10)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar (10)", nullable: true),
                    IntegrationId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationAccountDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntegrationAccountDetails_Integrations_IntegrationId",
                        column: x => x.IntegrationId,
                        principalTable: "Integrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MerchantProducts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TransactionProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentTransactionProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<long>(type: "bigint", nullable: true),
                    PriceIncTax = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: true),
                    DiscountAmount = table.Column<long>(type: "bigint", nullable: true),
                    TotalPrice = table.Column<long>(type: "bigint", nullable: true),
                    TaxPercent = table.Column<double>(type: "float", nullable: true),
                    TaxValue = table.Column<long>(type: "bigint", nullable: true),
                    TaxValueAfterDiscount = table.Column<long>(type: "bigint", nullable: true),
                    VariantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeightUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: true),
                    Sku = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ean = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Upc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarrantyExpiryDateTime = table.Column<long>(type: "bigint", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManualUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dimensions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductStatus = table.Column<int>(type: "int", nullable: false),
                    MerchantTransactionId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantProducts_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MerchantProducts_MerchantTransactions_MerchantTransactionId",
                        column: x => x.MerchantTransactionId,
                        principalTable: "MerchantTransactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MerchantRefunds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Shipping = table.Column<long>(type: "bigint", nullable: true),
                    Fee = table.Column<long>(type: "bigint", nullable: true),
                    Tax = table.Column<long>(type: "bigint", nullable: true),
                    Total = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar (250)", nullable: true),
                    MerchantTransactionId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantRefunds_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MerchantRefunds_MerchantTransactions_MerchantTransactionId",
                        column: x => x.MerchantTransactionId,
                        principalTable: "MerchantTransactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RelationshipDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    RelationshipHeaderId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ViewerEntityId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ViewerAirslipUserType = table.Column<int>(type: "int", nullable: false),
                    OwnerEntityId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    OwnerAirslipUserType = table.Column<int>(type: "int", nullable: false),
                    PermissionType = table.Column<string>(type: "nvarchar (50)", nullable: false),
                    Allowed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipDetails_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelationshipDetails_RelationshipHeaders_RelationshipHeaderId",
                        column: x => x.RelationshipHeaderId,
                        principalTable: "RelationshipHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountBalanceCreditLines",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AccountBalanceDetailId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    BalanceStatus = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar (5)", nullable: true),
                    CreditLineType = table.Column<int>(type: "int", nullable: false),
                    BankAccountBalanceDetailId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountBalanceCreditLines_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccountBalanceCreditLines_BankAccountBalanceDetails_BankAccountBalanceDetailId",
                        column: x => x.BankAccountBalanceDetailId,
                        principalTable: "BankAccountBalanceDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MerchantRefundItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TransactionProductId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ProductId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    VariantId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Qty = table.Column<double>(type: "float", nullable: true),
                    Refund = table.Column<long>(type: "bigint", nullable: true),
                    MerchantRefundId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantRefundItems_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MerchantRefundItems_MerchantRefunds_MerchantRefundId",
                        column: x => x.MerchantRefundId,
                        principalTable: "MerchantRefunds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBalanceCreditLines_BankAccountBalanceDetailId",
                table: "BankAccountBalanceCreditLines",
                column: "BankAccountBalanceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBalanceDetails_BankAccountBalanceId",
                table: "BankAccountBalanceDetails",
                column: "BankAccountBalanceId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBalances_AuditInformationId",
                table: "BankAccountBalances",
                column: "AuditInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_BankCountryCode_BankId",
                table: "BankCountryCode",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_AuditInformationId",
                table: "Banks",
                column: "AuditInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_BankSyncRequests_AuditInformationId",
                table: "BankSyncRequests",
                column: "AuditInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_AirslipUserType_EntityId_Day_Month_Year",
                table: "BankTransactions",
                columns: new[] { "AirslipUserType", "EntityId", "Day", "Month", "Year" })
                .Annotation("SqlServer:Include", new[] { "IntegrationId", "Amount", "BankId" });

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_AuditInformationId",
                table: "BankTransactions",
                column: "AuditInformationId",
                unique: true,
                filter: "[AuditInformationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_EntityId_AirslipUserType",
                table: "BankTransactions",
                columns: new[] { "EntityId", "AirslipUserType" })
                .Annotation("SqlServer:Include", new[] { "BankId" });

            migrationBuilder.CreateIndex(
                name: "IX_IntegrationAccountDetails_IntegrationId",
                table: "IntegrationAccountDetails",
                column: "IntegrationId",
                unique: true,
                filter: "[IntegrationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Integrations_AuditInformationId",
                table: "Integrations",
                column: "AuditInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantProducts_MerchantTransactionId",
                table: "MerchantProducts",
                column: "MerchantTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantRefundItems_MerchantRefundId",
                table: "MerchantRefundItems",
                column: "MerchantRefundId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantRefunds_MerchantTransactionId",
                table: "MerchantRefunds",
                column: "MerchantTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantTransactions_AuditInformationId",
                table: "MerchantTransactions",
                column: "AuditInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_RelationshipDetails_OwnerEntityId_OwnerAirslipUserType_ViewerEntityId_ViewerAirslipUserType_PermissionType_Allowed",
                table: "RelationshipDetails",
                columns: new[] { "OwnerEntityId", "OwnerAirslipUserType", "ViewerEntityId", "ViewerAirslipUserType", "PermissionType", "Allowed" });

            migrationBuilder.CreateIndex(
                name: "IX_RelationshipDetails_RelationshipHeaderId",
                table: "RelationshipDetails",
                column: "RelationshipHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_RelationshipHeaders_AuditInformationId",
                table: "RelationshipHeaders",
                column: "AuditInformationId");
            
            migrationBuilder.AddSqlFiles();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccountBalanceCreditLines");

            migrationBuilder.DropTable(
                name: "BankAccountBalanceSnapshots");

            migrationBuilder.DropTable(
                name: "BankAccountBalanceSummaries");

            migrationBuilder.DropTable(
                name: "BankAccountMetricSnapshots");

            migrationBuilder.DropTable(
                name: "BankBusinessBalances");

            migrationBuilder.DropTable(
                name: "BankBusinessBalanceSnapshots");

            migrationBuilder.DropTable(
                name: "BankCountryCode");

            migrationBuilder.DropTable(
                name: "BankSyncRequests");

            migrationBuilder.DropTable(
                name: "BankTransactions");

            migrationBuilder.DropTable(
                name: "CountryCodes");

            migrationBuilder.DropTable(
                name: "IntegrationAccountDetails");

            migrationBuilder.DropTable(
                name: "MerchantAccountMetricSnapshots");

            migrationBuilder.DropTable(
                name: "MerchantMetricSnapshots");

            migrationBuilder.DropTable(
                name: "MerchantProducts");

            migrationBuilder.DropTable(
                name: "MerchantRefundItems");

            migrationBuilder.DropTable(
                name: "RelationshipDetails");

            migrationBuilder.DropTable(
                name: "BankAccountBalanceDetails");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Integrations");

            migrationBuilder.DropTable(
                name: "MerchantRefunds");

            migrationBuilder.DropTable(
                name: "RelationshipHeaders");

            migrationBuilder.DropTable(
                name: "BankAccountBalances");

            migrationBuilder.DropTable(
                name: "MerchantTransactions");

            migrationBuilder.DropTable(
                name: "AuditInformation");
        }
    }
}
