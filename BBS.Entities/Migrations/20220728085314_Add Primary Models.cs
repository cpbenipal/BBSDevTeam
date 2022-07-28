using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class AddPrimaryModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BidOnPrimaryOfferings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    UserLoginId = table.Column<int>(type: "integer", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "integer", nullable: false),
                    VerificationStatus = table.Column<int>(type: "integer", nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: true),
                    PlacementAmount = table.Column<double>(type: "double precision", nullable: false),
                    TransactionId = table.Column<string>(type: "text", nullable: false),
                    ApprovedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidOnPrimaryOfferings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BidOnPrimaryOfferings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BidOnPrimaryOfferings_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BidOnPrimaryOfferings_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BidOnPrimaryOfferings_UserLogin_UserLoginId",
                        column: x => x.UserLoginId,
                        principalTable: "UserLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryOfferShareDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    BidOnPrimaryOfferingId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryOfferShareDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrimaryOfferShareDatas_BidOnPrimaryOfferings_BidOnPrimaryOf~",
                        column: x => x.BidOnPrimaryOfferingId,
                        principalTable: "BidOnPrimaryOfferings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrimaryOfferShareDatas_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrimaryOfferShareDatas_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BidOnPrimaryOfferings_CompanyId",
                table: "BidOnPrimaryOfferings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BidOnPrimaryOfferings_PaymentTypeId",
                table: "BidOnPrimaryOfferings",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BidOnPrimaryOfferings_StateId",
                table: "BidOnPrimaryOfferings",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_BidOnPrimaryOfferings_UserLoginId",
                table: "BidOnPrimaryOfferings",
                column: "UserLoginId");

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryOfferShareDatas_BidOnPrimaryOfferingId",
                table: "PrimaryOfferShareDatas",
                column: "BidOnPrimaryOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryOfferShareDatas_CategoryId",
                table: "PrimaryOfferShareDatas",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryOfferShareDatas_CompanyId",
                table: "PrimaryOfferShareDatas",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrimaryOfferShareDatas");

            migrationBuilder.DropTable(
                name: "BidOnPrimaryOfferings");
        }
    }
}
