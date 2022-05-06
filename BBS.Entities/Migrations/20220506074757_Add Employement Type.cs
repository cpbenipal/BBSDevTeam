using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class AddEmployementType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmployed",
                table: "Person");

            migrationBuilder.AddColumn<int>(
                name: "EmployementTypeId",
                table: "Person",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EmployementTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployementTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EmployementTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Employed" },
                    { 2, "Unemployed" },
                    { 3, "Full-Time" },
                    { 4, "Part-Time" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_EmployementTypeId",
                table: "Person",
                column: "EmployementTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_EmployementTypes_EmployementTypeId",
                table: "Person",
                column: "EmployementTypeId",
                principalTable: "EmployementTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_EmployementTypes_EmployementTypeId",
                table: "Person");

            migrationBuilder.DropTable(
                name: "EmployementTypes");

            migrationBuilder.DropIndex(
                name: "IX_Person_EmployementTypeId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "EmployementTypeId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Person");

            migrationBuilder.AddColumn<bool>(
                name: "IsEmployed",
                table: "Person",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
