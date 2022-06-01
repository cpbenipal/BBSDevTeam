using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class AddUserLogintoOfferPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserLoginId",
                table: "OfferPayments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OfferPayments_UserLoginId",
                table: "OfferPayments",
                column: "UserLoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferPayments_UserLogin_UserLoginId",
                table: "OfferPayments",
                column: "UserLoginId",
                principalTable: "UserLogin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferPayments_UserLogin_UserLoginId",
                table: "OfferPayments");

            migrationBuilder.DropIndex(
                name: "IX_OfferPayments_UserLoginId",
                table: "OfferPayments");

            migrationBuilder.DropColumn(
                name: "UserLoginId",
                table: "OfferPayments");
        }
    }
}
