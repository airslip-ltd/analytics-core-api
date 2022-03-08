using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class PartnerRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RelationshipHeaders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar (50)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "varchar (50)", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar (50)", nullable: true),
                    EntityId = table.Column<string>(type: "varchar (50)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    DataSource = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipHeaders_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelationshipHeaders_AuditInformation_AuditInformationId",
                        column: x => x.AuditInformationId,
                        principalTable: "AuditInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RelationshipDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar (50)", nullable: false),
                    RelationshipHeaderId = table.Column<string>(type: "varchar (50)", nullable: false),
                    ViewerEntityId = table.Column<string>(type: "varchar (50)", nullable: false),
                    ViewerAirslipUserType = table.Column<int>(type: "int", nullable: false),
                    OwnerEntityId = table.Column<string>(type: "varchar (50)", nullable: false),
                    OwnerAirslipUserType = table.Column<int>(type: "int", nullable: false),
                    PermissionType = table.Column<string>(type: "varchar (50)", nullable: false),
                    Allowed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipDetails_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelationshipDetails_RelationshipHeaders_RelationshipHeaderId",
                        column: x => x.RelationshipHeaderId,
                        principalTable: "RelationshipHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelationshipDetails_RelationshipHeaderId",
                table: "RelationshipDetails",
                column: "RelationshipHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_RelationshipHeaders_AuditInformationId",
                table: "RelationshipHeaders",
                column: "AuditInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelationshipDetails");

            migrationBuilder.DropTable(
                name: "RelationshipHeaders");
        }
    }
}
