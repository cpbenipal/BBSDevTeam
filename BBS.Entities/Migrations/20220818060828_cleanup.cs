using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class cleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferPrice",
                table: "SecondaryOfferShareDatas");

            migrationBuilder.DropColumn(
               name: "CategoryId",
               table: "SecondaryOfferShareDatas");

            migrationBuilder.DropColumn(
                name: "TotalShares",
                table: "SecondaryOfferShareDatas");

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OfferPrice",
                table: "SecondaryOfferShareDatas",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.DropColumn(
              name: "CategoryId",
              table: "SecondaryOfferShareDatas");

            migrationBuilder.AddColumn<int>(
                name: "TotalShares",
                table: "SecondaryOfferShareDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);
            
        }
    }
}
