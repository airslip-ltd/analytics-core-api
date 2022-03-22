using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class MissingIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RelationshipDetails_OwnerEntityId_OwnerAirslipUserType_ViewerEntityId_ViewerAirslipUserType_PermissionType_Allowed",
                table: "RelationshipDetails",
                columns: new[] { "OwnerEntityId", "OwnerAirslipUserType", "ViewerEntityId", "ViewerAirslipUserType", "PermissionType", "Allowed" });

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_AirslipUserType_EntityId_Day_Month_Year",
                table: "BankTransactions",
                columns: new[] { "AirslipUserType", "EntityId", "Day", "Month", "Year" })
                .Annotation("SqlServer:Include", new[] { "AccountId", "Amount", "BankId" });

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_EntityId_AirslipUserType",
                table: "BankTransactions",
                columns: new[] { "EntityId", "AirslipUserType" })
                .Annotation("SqlServer:Include", new[] { "BankId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RelationshipDetails_OwnerEntityId_OwnerAirslipUserType_ViewerEntityId_ViewerAirslipUserType_PermissionType_Allowed",
                table: "RelationshipDetails");

            migrationBuilder.DropIndex(
                name: "IX_BankTransactions_AirslipUserType_EntityId_Day_Month_Year",
                table: "BankTransactions");

            migrationBuilder.DropIndex(
                name: "IX_BankTransactions_EntityId_AirslipUserType",
                table: "BankTransactions");
        }
    }
}
