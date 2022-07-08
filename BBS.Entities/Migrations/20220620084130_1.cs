using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "IssuedDigitalShares");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "IssuedDigitalShares");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "IssuedDigitalShares");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "IssuedDigitalShares");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "IssuedDigitalShares");

            migrationBuilder.DropColumn(
                name: "NumberOfShares",
                table: "IssuedDigitalShares");

            migrationBuilder.AddColumn<string>(
                name: "GrantValuation",
                table: "Shares",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastValuation",
                table: "Shares",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrantValuation",
                table: "Shares");

            migrationBuilder.DropColumn(
                name: "LastValuation",
                table: "Shares");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "IssuedDigitalShares",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "IssuedDigitalShares",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "IssuedDigitalShares",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "IssuedDigitalShares",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "IssuedDigitalShares",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfShares",
                table: "IssuedDigitalShares",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
