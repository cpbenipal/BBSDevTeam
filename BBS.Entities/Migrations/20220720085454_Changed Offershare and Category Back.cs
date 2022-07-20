using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class ChangedOffershareandCategoryBack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Content", "Name", "OfferedShareMainTypeId" },
                values: new object[,]
                {
                    { 1, "", "Information", 1 },
                    { 2, "", "Deal Teaser", 1 },
                    { 3, "", "Team", 1 },
                    { 4, "", "Interviews", 1 },
                    { 5, "", "News", 1 },
                    { 6, "", "Company Profile", 2 },
                    { 7, "", "Deal Teaser", 2 },
                    { 8, "", "Terms And Legal", 2 },
                    { 9, "", "Documents", 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

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
    }
}
