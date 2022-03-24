using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class StatusColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "MerchantTransactions",
                type: "nvarchar (20)",
                nullable: false,
                defaultValue: "Unknown");

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "MerchantTransactions",
                type: "nvarchar (20)",
                nullable: false,
                defaultValue: "Unknown");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "MerchantTransactions");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "MerchantTransactions");
        }
    }
}
