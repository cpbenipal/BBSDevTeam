using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class AddOfferLimitTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "OfferTimeLimits",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 1, "3 Days" },
                    { 2, "1 Week" },
                    { 3, "3 Months" },
                    { 4, "6 Months" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OfferTimeLimits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OfferTimeLimits",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OfferTimeLimits",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OfferTimeLimits",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 1, "PaymentType 1" },
                    { 2, "PaymentType 2" }
                });
        }
    }
}
