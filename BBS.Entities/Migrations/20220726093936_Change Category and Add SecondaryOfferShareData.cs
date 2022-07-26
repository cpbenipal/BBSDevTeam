using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class ChangeCategoryandAddSecondaryOfferShareData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_OfferedShareId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "OfferPrice",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "OfferedShareId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "TotalShares",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "OfferedShareMainTypeId" },
                values: new object[,]
                {
                    { 1, "Information", 2 },
                    { 2, "Deal Teaser", 2 },
                    { 3, "Team", 2 },
                    { 4, "Interviews", 2 },
                    { 5, "News", 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "OfferPrice",
                table: "Categories",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfferedShareId",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalShares",
                table: "Categories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_OfferedShareId",
                table: "Categories",
                column: "OfferedShareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_OfferedShares_OfferedShareId",
                table: "Categories",
                column: "OfferedShareId",
                principalTable: "OfferedShares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
