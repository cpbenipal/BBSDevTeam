using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class AddUserLoginToOfferedShares : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserLoginId",
                table: "OfferedShares",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OfferedShares_UserLoginId",
                table: "OfferedShares",
                column: "UserLoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferedShares_UserLogin_UserLoginId",
                table: "OfferedShares",
                column: "UserLoginId",
                principalTable: "UserLogin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferedShares_UserLogin_UserLoginId",
                table: "OfferedShares");

            migrationBuilder.DropIndex(
                name: "IX_OfferedShares_UserLoginId",
                table: "OfferedShares");

            migrationBuilder.DropColumn(
                name: "UserLoginId",
                table: "OfferedShares");
        }
    }
}
