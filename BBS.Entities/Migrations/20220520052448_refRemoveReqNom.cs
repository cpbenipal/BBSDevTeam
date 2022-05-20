using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class refRemoveReqNom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Shares");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "IssuedDigitalShares");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Shares",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "IssuedDigitalShares",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
