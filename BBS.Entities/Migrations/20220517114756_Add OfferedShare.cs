using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class AddOfferedShare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OfferTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferedShares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IssuedDigitalShareId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    OfferPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    OfferTimeLimitInWeeks = table.Column<int>(type: "integer", nullable: false),
                    OfferTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferedShares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferedShares_IssuedDigitalShares_IssuedDigitalShareId",
                        column: x => x.IssuedDigitalShareId,
                        principalTable: "IssuedDigitalShares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferedShares_OfferTypes_OfferTypeId",
                        column: x => x.OfferTypeId,
                        principalTable: "OfferTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OfferTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Auction" },
                    { 2, "Private" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferedShares_IssuedDigitalShareId",
                table: "OfferedShares",
                column: "IssuedDigitalShareId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedShares_OfferTypeId",
                table: "OfferedShares",
                column: "OfferTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferedShares");

            migrationBuilder.DropTable(
                name: "OfferTypes");
        }
    }
}
