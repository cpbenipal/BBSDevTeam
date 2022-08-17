using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class RemovecategoriesAddBusraFeesAddTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedDate", "DateOfBirth", "DateOfEmployement", "ModifiedDate" },
                values: new object[] { 
                    new DateTime(2022, 8, 17, 13, 55, 56, 573, DateTimeKind.Utc).AddTicks(8148), 
                    new DateTime(2022, 8, 17, 10, 55, 56, 573, DateTimeKind.Utc).AddTicks(8150), 
                    new DateTime(2022, 8, 17, 10, 55, 56, 573, DateTimeKind.Utc).AddTicks(8155), 
                    new DateTime(2022, 8, 17, 13, 55, 56, 573, DateTimeKind.Utc).AddTicks(8148) 
                });

            migrationBuilder.UpdateData(
                table: "UserLogin",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedDate", "ModifiedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { 
                    new DateTime(2022, 8, 17, 13, 55, 56, 573, DateTimeKind.Utc).AddTicks(9370), 
                    new DateTime(2022, 8, 17, 13, 55, 56, 573, DateTimeKind.Utc).AddTicks(9372), 
                    new byte[] { 125, 255, 148, 207, 34, 238, 226, 41, 38, 108, 72, 131, 15, 225, 136, 190, 147, 196, 113, 234, 169, 206, 2, 85, 147, 105, 166, 199, 225, 101, 20, 39, 140, 254, 5, 145, 210, 219, 99, 158, 109, 242, 92, 227, 83, 149, 125, 88, 84, 60, 197, 92, 204, 100, 181, 200, 171, 174, 246, 6, 15, 188, 26, 253, 36, 114, 229, 142, 24, 153, 56, 59, 106, 34, 5, 16, 83, 92, 153, 249, 243, 54, 113, 97, 33, 98, 209, 159, 184, 109, 124, 244, 246, 57, 5, 164, 53, 118, 94, 202, 11, 233, 0, 33, 250, 109, 50, 194, 85, 130, 239, 129, 162, 92, 156, 57, 226, 32, 31, 177, 168, 144, 31, 168, 162, 155, 223, 49 }, new byte[] { 109, 250, 241, 63, 84, 35, 228, 203, 2, 165, 132, 59, 186, 68, 194, 81, 125, 165, 43, 243, 125, 233, 170, 217, 39, 111, 94, 13, 17, 139, 225, 244, 6, 210, 223, 181, 40, 53, 81, 223, 55, 70, 178, 181, 91, 19, 100, 222, 206, 126, 228, 162, 243, 142, 82, 138, 56, 190, 187, 21, 19, 137, 51, 215 } 
                });

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedDate", "ModifiedDate" },
                values: new object[] { 
                    new DateTime(2022, 8, 17, 13, 55, 56, 573, DateTimeKind.Utc).AddTicks(9398), 
                    new DateTime(2022, 8, 17, 13, 55, 56, 573, DateTimeKind.Utc).AddTicks(9399) 
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedDate", "DateOfBirth", "DateOfEmployement", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 8, 17, 11, 3, 11, 59, DateTimeKind.Local).AddTicks(5224), new DateTime(2022, 8, 17, 8, 3, 11, 59, DateTimeKind.Utc).AddTicks(5226), new DateTime(2022, 8, 17, 8, 3, 11, 59, DateTimeKind.Utc).AddTicks(5232), new DateTime(2022, 8, 17, 11, 3, 11, 59, DateTimeKind.Local).AddTicks(5225) });

            migrationBuilder.UpdateData(
                table: "UserLogin",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedDate", "ModifiedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 8, 17, 11, 3, 11, 59, DateTimeKind.Local).AddTicks(6351), new DateTime(2022, 8, 17, 11, 3, 11, 59, DateTimeKind.Local).AddTicks(6352), new byte[] { 238, 159, 42, 210, 136, 23, 133, 37, 58, 153, 212, 48, 45, 67, 137, 154, 32, 32, 55, 215, 66, 117, 25, 20, 117, 75, 248, 60, 66, 51, 138, 191, 21, 2, 69, 214, 174, 186, 84, 186, 100, 238, 190, 163, 6, 178, 32, 204, 204, 21, 206, 163, 46, 31, 108, 8, 195, 232, 144, 70, 150, 139, 71, 17, 70, 3, 98, 183, 236, 49, 11, 121, 212, 58, 40, 219, 223, 154, 230, 134, 17, 9, 51, 89, 46, 4, 99, 108, 64, 86, 252, 180, 139, 139, 189, 18, 36, 15, 33, 93, 244, 4, 143, 51, 165, 208, 164, 112, 87, 101, 21, 47, 238, 216, 170, 77, 100, 60, 220, 51, 69, 227, 181, 105, 118, 35, 10, 91 }, new byte[] { 29, 163, 143, 14, 25, 172, 29, 132, 7, 2, 25, 205, 111, 164, 65, 22, 162, 119, 224, 66, 183, 6, 25, 130, 153, 97, 253, 74, 25, 6, 172, 130, 247, 107, 31, 9, 87, 207, 112, 173, 159, 193, 152, 51, 167, 248, 239, 34, 54, 148, 170, 130, 245, 186, 168, 45, 97, 16, 17, 8, 137, 147, 172, 5 } });

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2022, 8, 17, 11, 3, 11, 59, DateTimeKind.Local).AddTicks(6376), new DateTime(2022, 8, 17, 11, 3, 11, 59, DateTimeKind.Local).AddTicks(6377) });
        }
    }
}
