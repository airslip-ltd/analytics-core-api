using Airslip.Analytics.Services.SqlServer.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class FixIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Update BankTransactions set IntegrationProviderId = BankId");
            
            migrationBuilder.DropColumn(
                name: "BankId",
                table: "BankTransactions");

            migrationBuilder.CreateIndex(
                name: "IX_Integrations_IntegrationProviderId",
                table: "Integrations",
                column: "IntegrationProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Integrations_IntegrationProviders_IntegrationProviderId",
                table: "Integrations",
                column: "IntegrationProviderId",
                principalTable: "IntegrationProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.AddSqlFiles();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Integrations_IntegrationProviders_IntegrationProviderId",
                table: "Integrations");

            migrationBuilder.DropIndex(
                name: "IX_Integrations_IntegrationProviderId",
                table: "Integrations");

            migrationBuilder.AddColumn<string>(
                name: "BankId",
                table: "BankTransactions",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");
        }
    }
}
