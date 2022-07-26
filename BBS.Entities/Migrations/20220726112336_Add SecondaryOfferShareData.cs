using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class AddSecondaryOfferShareData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SecondaryOfferShareDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    OfferedShareId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondaryOfferShareDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecondaryOfferShareDatas_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecondaryOfferShareDatas_OfferedShares_OfferedShareId",
                        column: x => x.OfferedShareId,
                        principalTable: "OfferedShares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryOfferShareDatas_CategoryId",
                table: "SecondaryOfferShareDatas",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryOfferShareDatas_OfferedShareId",
                table: "SecondaryOfferShareDatas",
                column: "OfferedShareId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecondaryOfferShareDatas");
        }
    }
}
