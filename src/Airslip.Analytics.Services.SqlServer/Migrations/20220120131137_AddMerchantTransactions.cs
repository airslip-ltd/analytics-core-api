using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class AddMerchantTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MerchantTransactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuditInformationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    DataSource = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    TrackingId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefundCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BankStatementDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankStatementTransactionIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreLocationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnlinePurchase = table.Column<bool>(type: "bit", nullable: true),
                    Subtotal = table.Column<long>(type: "bigint", nullable: true),
                    ServiceCharge = table.Column<long>(type: "bigint", nullable: true),
                    Total = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Till = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Store = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantTransactions_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MerchantTransactions_AuditInformation_AuditInformationId",
                        column: x => x.AuditInformationId,
                        principalTable: "AuditInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MerchantDiscounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<long>(type: "bigint", nullable: true),
                    MerchantTransactionId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantDiscounts_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MerchantDiscounts_MerchantTransactions_MerchantTransactionId",
                        column: x => x.MerchantTransactionId,
                        principalTable: "MerchantTransactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MerchantPaymentDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<long>(type: "bigint", nullable: true),
                    MerchantTransactionId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantPaymentDetails_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MerchantPaymentDetails_MerchantTransactions_MerchantTransactionId",
                        column: x => x.MerchantTransactionId,
                        principalTable: "MerchantTransactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MerchantProducts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subtotal = table.Column<long>(type: "bigint", nullable: true),
                    Total = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManualUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dimensions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<long>(type: "bigint", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sku = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ean = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Upc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VatCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VatRate = table.Column<decimal>(type: "smallmoney", nullable: true),
                    VatAmount = table.Column<long>(type: "bigint", nullable: true),
                    MerchantTransactionId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantProducts_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MerchantProducts_MerchantTransactions_MerchantTransactionId",
                        column: x => x.MerchantTransactionId,
                        principalTable: "MerchantTransactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MerchantVats",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<decimal>(type: "smallmoney", nullable: true),
                    Amount = table.Column<long>(type: "bigint", nullable: true),
                    MerchantTransactionId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantVats_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MerchantVats_MerchantTransactions_MerchantTransactionId",
                        column: x => x.MerchantTransactionId,
                        principalTable: "MerchantTransactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MerchantCardDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuthCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaskedPanNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PanSequence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardScheme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantPaymentDetailId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantCardDetails_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MerchantCardDetails_MerchantPaymentDetails_MerchantPaymentDetailId",
                        column: x => x.MerchantPaymentDetailId,
                        principalTable: "MerchantPaymentDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MerchantCardDetails_MerchantPaymentDetailId",
                table: "MerchantCardDetails",
                column: "MerchantPaymentDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantDiscounts_MerchantTransactionId",
                table: "MerchantDiscounts",
                column: "MerchantTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantPaymentDetails_MerchantTransactionId",
                table: "MerchantPaymentDetails",
                column: "MerchantTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantProducts_MerchantTransactionId",
                table: "MerchantProducts",
                column: "MerchantTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantTransactions_AuditInformationId",
                table: "MerchantTransactions",
                column: "AuditInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantVats_MerchantTransactionId",
                table: "MerchantVats",
                column: "MerchantTransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MerchantCardDetails");

            migrationBuilder.DropTable(
                name: "MerchantDiscounts");

            migrationBuilder.DropTable(
                name: "MerchantProducts");

            migrationBuilder.DropTable(
                name: "MerchantVats");

            migrationBuilder.DropTable(
                name: "MerchantPaymentDetails");

            migrationBuilder.DropTable(
                name: "MerchantTransactions");
        }
    }
}
