using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class AddPaymentTypeandOfferPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OfferTimeLimits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OfferTimeLimits",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OfferTimeLimits",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OfferTimeLimits",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaymentTypeId = table.Column<int>(type: "integer", nullable: false),
                    OfferedShareId = table.Column<int>(type: "integer", nullable: false),
                    TransactionId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferPayments_OfferedShares_OfferedShareId",
                        column: x => x.OfferedShareId,
                        principalTable: "OfferedShares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferPayments_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 1, "PaymentType 1" },
                    { 2, "PaymentType 2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferPayments_OfferedShareId",
                table: "OfferPayments",
                column: "OfferedShareId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferPayments_PaymentTypeId",
                table: "OfferPayments",
                column: "PaymentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferPayments");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.InsertData(
                table: "OfferTimeLimits",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 1, "3 Days" },
                    { 2, "1 Week" },
                    { 3, "3 Months" },
                    { 4, "6 Months" }
                });
        }
    }
}
