using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class AddPhoneNumberToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Person");
        }
    }
}
