using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class UseBankingTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UsageType",
                table: "IntegrationAccountDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar (50)");

            migrationBuilder.AlterColumn<int>(
                name: "AccountType",
                table: "IntegrationAccountDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar (50)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UsageType",
                table: "IntegrationAccountDetails",
                type: "nvarchar (50)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AccountType",
                table: "IntegrationAccountDetails",
                type: "nvarchar (50)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
