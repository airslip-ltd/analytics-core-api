using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class UseCommonBanking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccountBalanceCreditLines");

            migrationBuilder.DropTable(
                name: "BankAccountBalanceDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccountBalanceDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AccountBalanceId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    BalanceStatus = table.Column<int>(type: "int", nullable: false),
                    BalanceType = table.Column<int>(type: "int", nullable: false),
                    BankAccountBalanceId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreditLineIncluded = table.Column<bool>(type: "bit", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar (5)", nullable: true),
                    DateTime = table.Column<string>(type: "nvarchar (50)", nullable: true)
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
                name: "BankAccountBalanceCreditLines",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AccountBalanceDetailId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    BalanceStatus = table.Column<int>(type: "int", nullable: false),
                    BankAccountBalanceDetailId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreditLineType = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar (5)", nullable: true)
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
        }
    }
}
