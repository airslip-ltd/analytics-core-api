using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class SyncRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SyncRequests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsageType = table.Column<int>(type: "int", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    LastCardDigits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SyncStatus = table.Column<int>(type: "int", nullable: false),
                    RecordCount = table.Column<int>(type: "int", nullable: false),
                    TracingId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataSource = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncRequests_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SyncRequests_AuditInformation_AuditInformationId",
                        column: x => x.AuditInformationId,
                        principalTable: "AuditInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SyncRequests_AuditInformationId",
                table: "SyncRequests",
                column: "AuditInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SyncRequests");
        }
    }
}
