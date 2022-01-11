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
                name: "AccountBalances",
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
                    table.PrimaryKey("PK_AccountBalances_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountBalances_AuditInformation_AuditInformationId",
                        column: x => x.AuditInformationId,
                        principalTable: "AuditInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
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
                    table.PrimaryKey("PK_Accounts_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AuditInformation_AuditInformationId",
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
                name: "Transactions",
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
                    table.PrimaryKey("PK_Transactions_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_AuditInformation_AuditInformationId",
                        column: x => x.AuditInformationId,
                        principalTable: "AuditInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountBalanceDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountBalanceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BalanceType = table.Column<int>(type: "int", nullable: false),
                    CreditLineIncluded = table.Column<bool>(type: "bit", nullable: false),
                    DateTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BalanceStatus = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBalanceDetails_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountBalanceDetails_AccountBalances_AccountBalanceId",
                        column: x => x.AccountBalanceId,
                        principalTable: "AccountBalances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "AccountBalanceCreditLines",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountBalanceDetailId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BalanceStatus = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditLineType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBalanceCreditLines_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountBalanceCreditLines_AccountBalanceDetails_AccountBalanceDetailId",
                        column: x => x.AccountBalanceDetailId,
                        principalTable: "AccountBalanceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountBalanceCreditLines_AccountBalanceDetailId",
                table: "AccountBalanceCreditLines",
                column: "AccountBalanceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountBalanceDetails_AccountBalanceId",
                table: "AccountBalanceDetails",
                column: "AccountBalanceId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountBalances_AuditInformationId",
                table: "AccountBalances",
                column: "AuditInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AuditInformationId",
                table: "Accounts",
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
                name: "IX_Transactions_AuditInformationId",
                table: "Transactions",
                column: "AuditInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountBalanceCreditLines");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "BankCountryCode");

            migrationBuilder.DropTable(
                name: "CountryCodes");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "AccountBalanceDetails");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "AccountBalances");

            migrationBuilder.DropTable(
                name: "AuditInformation");
        }
    }
}
