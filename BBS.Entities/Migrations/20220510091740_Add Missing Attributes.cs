using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class AddMissingAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyInformationDocument",
                table: "Shares",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShareOwnershipDocument",
                table: "Shares",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployerName",
                table: "Person",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyInformationDocument",
                table: "Shares");

            migrationBuilder.DropColumn(
                name: "ShareOwnershipDocument",
                table: "Shares");

            migrationBuilder.DropColumn(
                name: "EmployerName",
                table: "Person");
        }
    }
}
