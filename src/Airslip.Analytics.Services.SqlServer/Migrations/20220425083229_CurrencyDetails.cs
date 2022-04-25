using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class CurrencyDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrencyDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    DefaultCulture = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyDetails_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyDetails_AuditInformation_AuditInformationId",
                        column: x => x.AuditInformationId,
                        principalTable: "AuditInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "CurrencyDetails",
                columns: new[] { "Id", "AuditInformationId", "DefaultCulture", "EntityStatus" },
                values: new object[,]
                {
                    { "AED", null, "ar-AE", 1 },
                    { "BGN", null, "bg-BG", 1 },
                    { "CHF", null, "de-CH", 1 },
                    { "CZK", null, "cs-CZ", 1 },
                    { "DKK", null, "da-DK", 1 },
                    { "EUR", null, "de-DE", 1 },
                    { "GBP", null, "en-GB", 1 },
                    { "HRK", null, "hr-HR", 1 },
                    { "HUF", null, "hu-HU", 1 },
                    { "ISK", null, "is-IS", 1 },
                    { "NGN", null, "en-NG", 1 },
                    { "NOK", null, "nb-NO", 1 },
                    { "PLN", null, "pl-PL", 1 },
                    { "RON", null, "ro-RO", 1 },
                    { "SEK", null, "sv-SE", 1 },
                    { "USD", null, "en-US", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyDetails_AuditInformationId",
                table: "CurrencyDetails",
                column: "AuditInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyDetails");
        }
    }
}
