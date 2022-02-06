using Airslip.Analytics.Services.SqlServer.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class AccountMetricSnapshots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddSqlFiles(nameof(AccountMetricSnapshots));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
