using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class FixIncorrectColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "BankBusinessBalances",
                type: "nvarchar (3)",
                nullable: false,
                defaultValue: "GBP",
                oldClrType: typeof(string),
                oldType: "nvarchar(3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "BankBusinessBalances",
                type: "nvarchar(3)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar (3)",
                oldDefaultValue: "GBP");
        }
    }
}
