using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class ChangedOffershareandCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferedShares_Categories_CategoryCompanyProfileId",
                table: "OfferedShares");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferedShares_Categories_CategoryDealTeaserId",
                table: "OfferedShares");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferedShares_Categories_CategoryDocumentsId",
                table: "OfferedShares");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferedShares_Categories_CategoryTagsId",
                table: "OfferedShares");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferedShares_Categories_CategoryTermsAndLegalId",
                table: "OfferedShares");

            migrationBuilder.DropIndex(
                name: "IX_OfferedShares_CategoryCompanyProfileId",
                table: "OfferedShares");

            migrationBuilder.DropIndex(
                name: "IX_OfferedShares_CategoryDealTeaserId",
                table: "OfferedShares");

            migrationBuilder.DropIndex(
                name: "IX_OfferedShares_CategoryDocumentsId",
                table: "OfferedShares");

            migrationBuilder.DropIndex(
                name: "IX_OfferedShares_CategoryTagsId",
                table: "OfferedShares");

            migrationBuilder.DropIndex(
                name: "IX_OfferedShares_CategoryTermsAndLegalId",
                table: "OfferedShares");

            migrationBuilder.DropColumn(
                name: "CategoryCompanyProfileId",
                table: "OfferedShares");

            migrationBuilder.DropColumn(
                name: "CategoryDealTeaserId",
                table: "OfferedShares");

            migrationBuilder.DropColumn(
                name: "CategoryDocumentsId",
                table: "OfferedShares");

            migrationBuilder.DropColumn(
                name: "CategoryTagsId",
                table: "OfferedShares");

            migrationBuilder.DropColumn(
                name: "CategoryTermsAndLegalId",
                table: "OfferedShares");

            migrationBuilder.DropColumn(
                name: "CompanyProfile",
                table: "OfferedShares");

            migrationBuilder.DropColumn(
                name: "DealTeaser",
                table: "OfferedShares");

            migrationBuilder.DropColumn(
                name: "Documents",
                table: "OfferedShares");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "OfferedShares");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "OfferedShares");

            migrationBuilder.DropColumn(
                name: "TermsAndLegal",
                table: "OfferedShares");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Categories",
                newName: "TermsAndLegal");

            migrationBuilder.AddColumn<string>(
                name: "CompanyProfile",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DealTeaser",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Documents",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyProfile",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DealTeaser",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Documents",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "TermsAndLegal",
                table: "Categories",
                newName: "Content");

            migrationBuilder.AddColumn<int>(
                name: "CategoryCompanyProfileId",
                table: "OfferedShares",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryDealTeaserId",
                table: "OfferedShares",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryDocumentsId",
                table: "OfferedShares",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryTagsId",
                table: "OfferedShares",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryTermsAndLegalId",
                table: "OfferedShares",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyProfile",
                table: "OfferedShares",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DealTeaser",
                table: "OfferedShares",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Documents",
                table: "OfferedShares",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OfferedShares",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tags",
                table: "OfferedShares",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TermsAndLegal",
                table: "OfferedShares",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OfferedShares_CategoryCompanyProfileId",
                table: "OfferedShares",
                column: "CategoryCompanyProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedShares_CategoryDealTeaserId",
                table: "OfferedShares",
                column: "CategoryDealTeaserId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedShares_CategoryDocumentsId",
                table: "OfferedShares",
                column: "CategoryDocumentsId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedShares_CategoryTagsId",
                table: "OfferedShares",
                column: "CategoryTagsId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedShares_CategoryTermsAndLegalId",
                table: "OfferedShares",
                column: "CategoryTermsAndLegalId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferedShares_Categories_CategoryCompanyProfileId",
                table: "OfferedShares",
                column: "CategoryCompanyProfileId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferedShares_Categories_CategoryDealTeaserId",
                table: "OfferedShares",
                column: "CategoryDealTeaserId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferedShares_Categories_CategoryDocumentsId",
                table: "OfferedShares",
                column: "CategoryDocumentsId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferedShares_Categories_CategoryTagsId",
                table: "OfferedShares",
                column: "CategoryTagsId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferedShares_Categories_CategoryTermsAndLegalId",
                table: "OfferedShares",
                column: "CategoryTermsAndLegalId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
