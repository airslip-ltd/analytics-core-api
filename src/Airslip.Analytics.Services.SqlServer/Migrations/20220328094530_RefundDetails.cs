using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class RefundDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TotalRefund",
                table: "MerchantTransactions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "QuantityRefunded",
                table: "MerchantProducts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TotalRefund",
                table: "MerchantProducts",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalRefund",
                table: "MerchantTransactions");

            migrationBuilder.DropColumn(
                name: "QuantityRefunded",
                table: "MerchantProducts");

            migrationBuilder.DropColumn(
                name: "TotalRefund",
                table: "MerchantProducts");
        }
    }
}
