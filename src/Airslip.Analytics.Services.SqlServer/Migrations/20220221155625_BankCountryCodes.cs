using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class BankCountryCodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BankTransactions_AuditInformationId",
                table: "BankTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankCountryCode",
                table: "BankCountryCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankCountryCode",
                table: "BankCountryCode",
                columns: new[] { "Id", "BankId" });

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_AuditInformationId",
                table: "BankTransactions",
                column: "AuditInformationId",
                unique: true,
                filter: "[AuditInformationId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BankTransactions_AuditInformationId",
                table: "BankTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankCountryCode",
                table: "BankCountryCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankCountryCode",
                table: "BankCountryCode",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_AuditInformationId",
                table: "BankTransactions",
                column: "AuditInformationId");
        }
    }
}
