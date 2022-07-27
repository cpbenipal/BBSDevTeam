using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class tlshareandpricefieldsaddedtosecoffrdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AddColumn<decimal>(
                name: "OfferPrice",
                table: "SecondaryOfferShareDatas",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TotalShares",
                table: "SecondaryOfferShareDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferPrice",
                table: "SecondaryOfferShareDatas");

            migrationBuilder.DropColumn(
                name: "TotalShares",
                table: "SecondaryOfferShareDatas");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "OfferedShareMainTypeId" },
                values: new object[] { 5, "News", 2 });
        }
    }
}
