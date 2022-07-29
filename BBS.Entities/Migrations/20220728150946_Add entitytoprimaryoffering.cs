using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class Addentitytoprimaryoffering : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "AddedById",
                table: "PrimaryOfferShareDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "PrimaryOfferShareDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "PrimaryOfferShareDatas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PrimaryOfferShareDatas",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "PrimaryOfferShareDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "PrimaryOfferShareDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "AddedById",
                table: "Companies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Companies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Companies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "AddedById",
                table: "BidOnPrimaryOfferings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "BidOnPrimaryOfferings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "BidOnPrimaryOfferings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BidOnPrimaryOfferings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "BidOnPrimaryOfferings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "BidOnPrimaryOfferings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "PrimaryOfferShareDatas");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "PrimaryOfferShareDatas");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "PrimaryOfferShareDatas");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PrimaryOfferShareDatas");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "PrimaryOfferShareDatas");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "PrimaryOfferShareDatas");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "BidOnPrimaryOfferings");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "BidOnPrimaryOfferings");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "BidOnPrimaryOfferings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BidOnPrimaryOfferings");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "BidOnPrimaryOfferings");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "BidOnPrimaryOfferings");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "OfferedShareMainTypeId" },
                values: new object[,]
                {
                    { 1, "Information", 2 },
                    { 2, "Deal Teaser", 2 },
                    { 3, "Team", 2 },
                    { 4, "Interviews", 2 }
                });
        }
    }
}
