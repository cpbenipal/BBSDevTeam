using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class AddCertificateKeyToShare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CertificateKey",
                table: "IssuedDigitalShares",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificateKey",
                table: "IssuedDigitalShares");
        }
    }
}
