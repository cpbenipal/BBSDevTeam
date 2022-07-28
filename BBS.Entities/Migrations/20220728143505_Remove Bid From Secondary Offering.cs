using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class RemoveBidFromSecondaryOffering : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrimaryOfferShareDatas_BidOnPrimaryOfferings_BidOnPrimaryOf~",
                table: "PrimaryOfferShareDatas");

            migrationBuilder.DropIndex(
                name: "IX_PrimaryOfferShareDatas_BidOnPrimaryOfferingId",
                table: "PrimaryOfferShareDatas");

            migrationBuilder.DropColumn(
                name: "BidOnPrimaryOfferingId",
                table: "PrimaryOfferShareDatas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BidOnPrimaryOfferingId",
                table: "PrimaryOfferShareDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryOfferShareDatas_BidOnPrimaryOfferingId",
                table: "PrimaryOfferShareDatas",
                column: "BidOnPrimaryOfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrimaryOfferShareDatas_BidOnPrimaryOfferings_BidOnPrimaryOf~",
                table: "PrimaryOfferShareDatas",
                column: "BidOnPrimaryOfferingId",
                principalTable: "BidOnPrimaryOfferings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
