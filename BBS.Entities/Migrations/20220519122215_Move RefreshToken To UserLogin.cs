using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class MoveRefreshTokenToUserLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Person");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "UserLogin",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "UserLogin");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
