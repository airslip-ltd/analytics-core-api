using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class CreateIntegrationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankCountryCode");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropIndex(
                name: "IX_BankTransactions_AirslipUserType_EntityId_Day_Month_Year",
                table: "BankTransactions");

            migrationBuilder.DropIndex(
                name: "IX_BankTransactions_EntityId_AirslipUserType",
                table: "BankTransactions");

            migrationBuilder.AddColumn<string>(
                name: "IntegrationProviderId",
                table: "BankTransactions",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "IntegrationProviders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar (150)", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar (50)", nullable: false),
                    FriendlyName = table.Column<string>(type: "nvarchar (150)", nullable: false),
                    IntegrationType = table.Column<int>(type: "int", nullable: false),
                    EnvironmentType = table.Column<int>(type: "int", nullable: false),
                    Integration = table.Column<string>(type: "nvarchar (50)", nullable: false, defaultValue: "hub"),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    DataSource = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationProviders_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntegrationProviders_AuditInformation_AuditInformationId",
                        column: x => x.AuditInformationId,
                        principalTable: "AuditInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_AirslipUserType_EntityId_Day_Month_Year",
                table: "BankTransactions",
                columns: new[] { "AirslipUserType", "EntityId", "Day", "Month", "Year" })
                .Annotation("SqlServer:Include", new[] { "IntegrationId", "Amount", "IntegrationProviderId" });

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_EntityId_AirslipUserType",
                table: "BankTransactions",
                columns: new[] { "EntityId", "AirslipUserType" })
                .Annotation("SqlServer:Include", new[] { "IntegrationProviderId" });

            migrationBuilder.CreateIndex(
                name: "IX_IntegrationProviders_AuditInformationId",
                table: "IntegrationProviders",
                column: "AuditInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntegrationProviders");

            migrationBuilder.DropIndex(
                name: "IX_BankTransactions_AirslipUserType_EntityId_Day_Month_Year",
                table: "BankTransactions");

            migrationBuilder.DropIndex(
                name: "IX_BankTransactions_EntityId_AirslipUserType",
                table: "BankTransactions");

            migrationBuilder.DropColumn(
                name: "IntegrationProviderId",
                table: "BankTransactions");

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    AccountName = table.Column<string>(type: "nvarchar (50)", nullable: false),
                    DataSource = table.Column<int>(type: "int", nullable: false),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    EnvironmentType = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    TradingName = table.Column<string>(type: "nvarchar (50)", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_AirslipUserType_EntityId_Day_Month_Year",
                table: "BankTransactions",
                columns: new[] { "AirslipUserType", "EntityId", "Day", "Month", "Year" })
                .Annotation("SqlServer:Include", new[] { "IntegrationId", "Amount", "BankId" });

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_EntityId_AirslipUserType",
                table: "BankTransactions",
                columns: new[] { "EntityId", "AirslipUserType" })
                .Annotation("SqlServer:Include", new[] { "BankId" });

            migrationBuilder.CreateIndex(
                name: "IX_BankCountryCode_BankId",
                table: "BankCountryCode",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_AuditInformationId",
                table: "Banks",
                column: "AuditInformationId");
        }
    }
}
