using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class CountryCodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuditInformationId",
                table: "CountryCodes",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryCode3Characters",
                table: "CountryCodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "CountryCodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DefaultCulture",
                table: "CountryCodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EntityStatus",
                table: "CountryCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CountryCodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "AuditInformationId", "CountryCode3Characters", "CurrencyCode", "DefaultCulture", "EntityStatus", "Name" },
                values: new object[,]
                {
                    { "AE", null, "ARE", "AED", "ar-AE", 1, "United Arab Emirates" },
                    { "AT", null, "AUT", "EUR", "de-AT", 1, "Austria" },
                    { "BE", null, "BEL", "EUR", "fr-BE", 1, "Belgium" },
                    { "BG", null, "BGR", "BGN", "bg-BG", 1, "Bulgaria" },
                    { "CH", null, "CHE", "CHF", "de-CH", 1, "Switzerland" },
                    { "CY", null, "CYP", "EUR", "el-CY", 1, "Cyprus" },
                    { "CZ", null, "CZE", "CZK", "cs-CZ", 1, "Czech Republic" },
                    { "DE", null, "DEU", "EUR", "de-DE", 1, "Germany" },
                    { "DK", null, "DNK", "DKK", "da-DK", 1, "Denmark" },
                    { "EE", null, "EST", "EUR", "et-EE", 1, "Estonia" },
                    { "ES", null, "ESP", "EUR", "es-ES", 1, "Spain" },
                    { "FI", null, "FIN", "EUR", "fi-FI", 1, "Finland" },
                    { "FR", null, "FRA", "EUR", "fr-FR", 1, "France" },
                    { "GB", null, "GBR", "GBP", "en-GB", 1, "United Kingdom" },
                    { "GR", null, "GRC", "EUR", "el-GR", 1, "Greece" },
                    { "HR", null, "HRV", "HRK", "hr-HR", 1, "Croatia" },
                    { "HU", null, "HUN", "HUF", "hu-HU", 1, "Hungary" },
                    { "IE", null, "IRL", "EUR", "en-IE", 1, "Ireland" },
                    { "IS", null, "ISL", "ISK", "is-IS", 1, "Iceland" },
                    { "IT", null, "ITA", "EUR", "it-IT", 1, "Italy" },
                    { "LI", null, "LIE", "CHF", "de-LI", 1, "Liechtenstein" },
                    { "LT", null, "LTU", "EUR", "lt-LT", 1, "Lithuania" },
                    { "LU", null, "LUX", "EUR", "de-LU", 1, "Luxembourg" },
                    { "LV", null, "LVA", "EUR", "lv-LV", 1, "Latvia" },
                    { "MT", null, "MLT", "EUR", "mt-MT", 1, "Malta" },
                    { "NG", null, "NGA", "NGN", "en-NG", 1, "Nigeria" },
                    { "NL", null, "NLD", "EUR", "nl-NL", 1, "Netherlands" },
                    { "NO", null, "NOR", "NOK", "nb-NO", 1, "Norway" },
                    { "PL", null, "POL", "PLN", "pl-PL", 1, "Poland" },
                    { "PT", null, "PRT", "EUR", "pt-PT", 1, "Portugal" },
                    { "RO", null, "ROU", "RON", "ro-RO", 1, "Romania" },
                    { "SE", null, "SWE", "SEK", "sv-SE", 1, "Sweden" },
                    { "SI", null, "SVN", "EUR", "sl-SI", 1, "Slovenia" },
                    { "SK", null, "SVK", "EUR", "sk-SK", 1, "Slovakia" },
                    { "US", null, "USA", "USD", "en-US", 1, "United States" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryCodes_AuditInformationId",
                table: "CountryCodes",
                column: "AuditInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CountryCodes_AuditInformation_AuditInformationId",
                table: "CountryCodes",
                column: "AuditInformationId",
                principalTable: "AuditInformation",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountryCodes_AuditInformation_AuditInformationId",
                table: "CountryCodes");

            migrationBuilder.DropIndex(
                name: "IX_CountryCodes_AuditInformationId",
                table: "CountryCodes");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "AE");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "AT");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "BE");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "BG");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "CH");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "CY");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "CZ");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "DE");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "DK");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "EE");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "ES");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "FI");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "FR");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "GB");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "GR");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "HR");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "HU");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "IE");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "IS");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "IT");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "LI");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "LT");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "LU");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "LV");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "MT");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "NG");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "NL");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "NO");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "PL");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "PT");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "RO");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "SE");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "SI");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "SK");

            migrationBuilder.DeleteData(
                table: "CountryCodes",
                keyColumn: "Id",
                keyValue: "US");

            migrationBuilder.DropColumn(
                name: "AuditInformationId",
                table: "CountryCodes");

            migrationBuilder.DropColumn(
                name: "CountryCode3Characters",
                table: "CountryCodes");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "CountryCodes");

            migrationBuilder.DropColumn(
                name: "DefaultCulture",
                table: "CountryCodes");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "CountryCodes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CountryCodes");
        }
    }
}
