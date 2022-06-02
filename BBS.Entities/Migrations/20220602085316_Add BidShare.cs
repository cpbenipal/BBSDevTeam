using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class AddBidShare : Migration
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
                name: "BidShares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    MaximumBidPrice = table.Column<double>(type: "double precision", nullable: false),
                    MinimumBidPrice = table.Column<double>(type: "double precision", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "integer", nullable: false),
                    OfferedShareId = table.Column<int>(type: "integer", nullable: false),
                    UserLoginId = table.Column<int>(type: "integer", nullable: false),
                    VerificationStateId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidShares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BidShares_OfferedShares_OfferedShareId",
                        column: x => x.OfferedShareId,
                        principalTable: "OfferedShares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BidShares_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BidShares_States_VerificationStateId",
                        column: x => x.VerificationStateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BidShares_UserLogin_UserLoginId",
                        column: x => x.UserLoginId,
                        principalTable: "UserLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BidShares_OfferedShareId",
                table: "BidShares",
                column: "OfferedShareId");

            migrationBuilder.CreateIndex(
                name: "IX_BidShares_PaymentTypeId",
                table: "BidShares",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BidShares_UserLoginId",
                table: "BidShares",
                column: "UserLoginId");

            migrationBuilder.CreateIndex(
                name: "IX_BidShares_VerificationStateId",
                table: "BidShares",
                column: "VerificationStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BidShares");

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
