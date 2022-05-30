﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BBS.Entities.Migrations
{
    public partial class AddPrivateOfferTypeKeyinOfferShare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DebtRounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebtRounds", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "EquityRounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquityRounds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrantTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrantTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restrictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restrictions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsUSCitizen = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublicSectorEmployee = table.Column<bool>(type: "boolean", nullable: false),
                    IsIndividual = table.Column<bool>(type: "boolean", nullable: false),
                    HaveCriminalRecord = table.Column<bool>(type: "boolean", nullable: false),
                    HaveConvicted = table.Column<bool>(type: "boolean", nullable: false),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AddressLine = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EmiratesID = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    VaultNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IBANNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    NationalityId = table.Column<int>(type: "integer", nullable: false),
                    EmployementTypeId = table.Column<int>(type: "integer", nullable: false),
                    EmployerName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AnnualIncome = table.Column<decimal>(type: "numeric", nullable: false),
                    DateOfEmployement = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HavePriorExpirence = table.Column<bool>(type: "boolean", nullable: false),
                    HaveTraining = table.Column<bool>(type: "boolean", nullable: false),
                    HaveExperience = table.Column<bool>(type: "boolean", nullable: false),
                    VerificationState = table.Column<int>(type: "integer", nullable: false),
                    AddedById = table.Column<int>(type: "integer", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IPAddress = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Person_EmployementTypes_EmployementTypeId",
                        column: x => x.EmployementTypeId,
                        principalTable: "EmployementTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Person_Nationality_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Front = table.Column<string>(type: "text", nullable: false),
                    Back = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    AddedById = table.Column<int>(type: "integer", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IPAddress = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalAttachments_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Passcode = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: true),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: false),
                    AddedById = table.Column<int>(type: "integer", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IPAddress = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogin_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssuedDigitalShares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShareId = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NumberOfShares = table.Column<int>(type: "integer", nullable: false),
                    IsCertified = table.Column<bool>(type: "boolean", nullable: false),
                    UserLoginId = table.Column<int>(type: "integer", nullable: false),
                    CertificateUrl = table.Column<string>(type: "text", nullable: false),
                    CertificateKey = table.Column<string>(type: "text", nullable: false),
                    AddedById = table.Column<int>(type: "integer", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IPAddress = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuedDigitalShares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssuedDigitalShares_UserLogin_UserLoginId",
                        column: x => x.UserLoginId,
                        principalTable: "UserLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyName = table.Column<string>(type: "text", nullable: true),
                    GrantTypeId = table.Column<int>(type: "integer", nullable: false),
                    EquityRoundId = table.Column<int>(type: "integer", nullable: false),
                    DebtRoundId = table.Column<int>(type: "integer", nullable: false),
                    NumberOfShares = table.Column<int>(type: "integer", nullable: false),
                    DateOfGrant = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SharePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Restriction1 = table.Column<bool>(type: "boolean", nullable: false),
                    Restriction2 = table.Column<bool>(type: "boolean", nullable: false),
                    StorageLocationId = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    BusinessLogo = table.Column<string>(type: "text", nullable: true),
                    ShareOwnershipDocument = table.Column<string>(type: "text", nullable: true),
                    CompanyInformationDocument = table.Column<string>(type: "text", nullable: true),
                    UserLoginId = table.Column<int>(type: "integer", nullable: false),
                    AddedById = table.Column<int>(type: "integer", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IPAddress = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shares_UserLogin_UserLoginId",
                        column: x => x.UserLoginId,
                        principalTable: "UserLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    UserLoginId = table.Column<int>(type: "integer", nullable: false),
                    AddedById = table.Column<int>(type: "integer", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IPAddress = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_UserLogin_UserLoginId",
                        column: x => x.UserLoginId,
                        principalTable: "UserLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferedShares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IssuedDigitalShareId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    PrivateShareKey = table.Column<string>(type: "text", nullable: true),
                    OfferPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    OfferTimeLimitInWeeks = table.Column<int>(type: "integer", nullable: false),
                    OfferTypeId = table.Column<int>(type: "integer", nullable: false),
                    UserLoginId = table.Column<int>(type: "integer", nullable: false),
                    AddedById = table.Column<int>(type: "integer", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IPAddress = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferedShares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferedShares_IssuedDigitalShares_IssuedDigitalShareId",
                        column: x => x.IssuedDigitalShareId,
                        principalTable: "IssuedDigitalShares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferedShares_OfferTypes_OfferTypeId",
                        column: x => x.OfferTypeId,
                        principalTable: "OfferTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferedShares_UserLogin_UserLoginId",
                        column: x => x.UserLoginId,
                        principalTable: "UserLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "India" },
                    { 2, "UAE" }
                });

            migrationBuilder.InsertData(
                table: "DebtRounds",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mezanine" },
                    { 2, "Growth" }
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

            migrationBuilder.InsertData(
                table: "EquityRounds",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Angel" },
                    { 2, "Seed" },
                    { 3, "Pre-Seed" },
                    { 4, "Serie A" },
                    { 5, "Serie B" },
                    { 6, "Serie C" }
                });

            migrationBuilder.InsertData(
                table: "GrantTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Normal Shares for ownership, voting, and share price appreciation", "Common" },
                    { 2, "Hybrid shares for ownership, non-voting, but its shareholdres and paid dividends prior to other shareholders", "Preffered" },
                    { 3, "An obligation to offer dividend or interest which typically includes a promise to convert to equity. Debt holdres get paid back before any other shareholder", "Debt" }
                });

            migrationBuilder.InsertData(
                table: "Nationality",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Indian" },
                    { 2, "Emirati" }
                });

            migrationBuilder.InsertData(
                table: "OfferTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Auction" },
                    { 2, "Private" }
                });

            migrationBuilder.InsertData(
                table: "Restrictions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "To the best of your knwledge, are there any sale or transfer restrictions related to these shares ? Are you a cofounder or employee at the company ? " },
                    { 2, "Are you a cofounder or employee at the company?" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Investor" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[,]
                {
                    { 1, "Pending", 0 },
                    { 2, "Returned", 1 },
                    { 3, "Completed", 2 }
                });

            migrationBuilder.InsertData(
                table: "StorageLocations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Dropbox" },
                    { 2, "One Drive" },
                    { 3, "Google " },
                    { 4, "iCloud" },
                    { 5, "My Desktop" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssuedDigitalShares_UserLoginId",
                table: "IssuedDigitalShares",
                column: "UserLoginId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedShares_IssuedDigitalShareId",
                table: "OfferedShares",
                column: "IssuedDigitalShareId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedShares_OfferTypeId",
                table: "OfferedShares",
                column: "OfferTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedShares_UserLoginId",
                table: "OfferedShares",
                column: "UserLoginId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_CountryId",
                table: "Person",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_EmployementTypeId",
                table: "Person",
                column: "EmployementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_NationalityId",
                table: "Person",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalAttachments_PersonId",
                table: "PersonalAttachments",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Shares_UserLoginId",
                table: "Shares",
                column: "UserLoginId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_PersonId",
                table: "UserLogin",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserLoginId",
                table: "UserRole",
                column: "UserLoginId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "DebtRounds");

            migrationBuilder.DropTable(
                name: "EquityRounds");

            migrationBuilder.DropTable(
                name: "GrantTypes");

            migrationBuilder.DropTable(
                name: "OfferedShares");

            migrationBuilder.DropTable(
                name: "PersonalAttachments");

            migrationBuilder.DropTable(
                name: "Restrictions");

            migrationBuilder.DropTable(
                name: "Shares");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "StorageLocations");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "IssuedDigitalShares");

            migrationBuilder.DropTable(
                name: "OfferTypes");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "EmployementTypes");

            migrationBuilder.DropTable(
                name: "Nationality");
        }
    }
}
