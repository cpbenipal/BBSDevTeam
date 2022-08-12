using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class chgprioffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrimaryOfferShareDatas_Categories_CategoryId",
                table: "PrimaryOfferShareDatas");

            migrationBuilder.DropIndex(
                name: "IX_PrimaryOfferShareDatas_CategoryId",
                table: "PrimaryOfferShareDatas");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PrimaryOfferShareDatas");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "PrimaryOfferShareDatas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosingDate",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvestmentManager",
                table: "Companies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumInvestment",
                table: "Companies",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalTargetAmount",
                table: "Companies",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);


          

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Name",
                table: "Companies",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Companies_Name",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "PrimaryOfferShareDatas");

            migrationBuilder.DropColumn(
                name: "ClosingDate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "InvestmentManager",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "MinimumInvestment",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "TotalTargetAmount",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "PrimaryOfferShareDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsWebView", "Name", "OfferedShareMainTypeId" },
                values: new object[,]
                {
                    { 5, false, "Tags", 1 },
                    { 6, false, "Short Description", 1 },
                    { 7, true, "Deal Teaser", 1 },
                    { 8, true, "Company Profile", 1 },
                    { 9, true, "Terms & Legal", 1 },
                    { 10, true, "Documents", 1 },
                    { 11, false, "Minumum Investment", 1 },
                    { 12, false, "Closing Date", 1 },
                    { 13, false, "Investment Manager", 1 },
                    { 14, false, "Fees in %", 1 }
                });

           
            migrationBuilder.CreateIndex(
                name: "IX_PrimaryOfferShareDatas_CategoryId",
                table: "PrimaryOfferShareDatas",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrimaryOfferShareDatas_Categories_CategoryId",
                table: "PrimaryOfferShareDatas",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
