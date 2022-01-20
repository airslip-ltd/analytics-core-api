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
            migrationBuilder.AddSqlFiles("Core");
            
            migrationBuilder.CreateTable(
                name: "AuditInformation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountBalanceSnapshots_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountBalanceSummaries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "dbo.getId()"),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    Movement = table.Column<double>(type: "float", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountBalanceSummaries_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankBusinessBalances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "dbo.getId()"),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    Movement = table.Column<double>(type: "float", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankBusinessBalances_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankBusinessBalanceSnapshots",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankBusinessBalanceSnapshots_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryCodes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCodes_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountBalances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BalanceStatus = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    AccountStatus = table.Column<int>(type: "int", nullable: false),
                    DataSource = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    LastCardDigits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstitutionId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_AuditInformation_AuditInformationId",
                        column: x => x.AuditInformationId,
                        principalTable: "AuditInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    TradingName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnvironmentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsageType = table.Column<int>(type: "int", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    LastCardDigits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SyncStatus = table.Column<int>(type: "int", nullable: false),
                    RecordCount = table.Column<int>(type: "int", nullable: false),
                    TracingId = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    BankTransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorisedDate = table.Column<long>(type: "bigint", nullable: true),
                    CapturedDate = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastCardDigits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsoFamilyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProprietaryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataSource = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false)
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
                name: "BankAccountBalanceDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountBalanceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BalanceType = table.Column<int>(type: "int", nullable: false),
                    CreditLineIncluded = table.Column<bool>(type: "bit", nullable: false),
                    DateTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BalanceStatus = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountBalanceId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                    BankId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCountryCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankCountryCode_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountBalanceCreditLines",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountBalanceDetailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BalanceStatus = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditLineType = table.Column<int>(type: "int", nullable: false),
                    BankAccountBalanceDetailId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                name: "IX_BankAccounts_AuditInformationId",
                table: "BankAccounts",
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
                name: "IX_BankTransactions_AuditInformationId",
                table: "BankTransactions",
                column: "AuditInformationId");
            
            migrationBuilder.AddSqlFiles(nameof(InitialCreate));
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
                name: "BankAccounts");

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
                name: "BankAccountBalanceDetails");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "BankAccountBalances");

            migrationBuilder.DropTable(
                name: "AuditInformation");
        }
    }
}
