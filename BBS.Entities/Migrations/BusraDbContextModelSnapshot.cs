﻿// <auto-generated />
using System;
using BBS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BBS.Entities.Migrations
{
    [DbContext(typeof(BusraDbContext))]
    partial class BusraDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BBS.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("BBS.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Country");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "India"
                        },
                        new
                        {
                            Id = 2,
                            Name = "UAE"
                        });
                });

            modelBuilder.Entity("BBS.Models.DebtRound", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DebtRounds");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Mezanine"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Growth"
                        });
                });

            modelBuilder.Entity("BBS.Models.EmployementType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EmployementTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Employed"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Unemployed"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Full-Time"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Part-Time"
                        });
                });

            modelBuilder.Entity("BBS.Models.EquityRound", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EquityRounds");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Angel"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Seed"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Pre-Seed"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Serie A"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Serie B"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Serie C"
                        });
                });

            modelBuilder.Entity("BBS.Models.GrantType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("GrantTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Normal Shares for ownership, voting, and share price appreciation",
                            Name = "Common"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Hybrid shares for ownership, non-voting, but its shareholdres and paid dividends prior to other shareholders",
                            Name = "Preffered"
                        },
                        new
                        {
                            Id = 3,
                            Description = "An obligation to offer dividend or interest which typically includes a promise to convert to equity. Debt holdres get paid back before any other shareholder",
                            Name = "Debt"
                        });
                });

            modelBuilder.Entity("BBS.Models.IssuedDigitalShare", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CertificateKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CertificateUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("IPAddress")
                        .HasColumnType("text");

                    b.Property<bool>("IsCertified")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("ModifiedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("NumberOfShares")
                        .HasColumnType("integer");

                    b.Property<int>("ShareId")
                        .HasColumnType("integer");

                    b.Property<int>("UserLoginId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserLoginId");

                    b.ToTable("IssuedDigitalShares");
                });

            modelBuilder.Entity("BBS.Models.Nationality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Nationality");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Indian"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Emirati"
                        });
                });

            modelBuilder.Entity("BBS.Models.OfferedShare", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IPAddress")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("IssuedDigitalShareId")
                        .HasColumnType("integer");

                    b.Property<int>("ModifiedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("OfferPrice")
                        .HasColumnType("numeric");

                    b.Property<int>("OfferTimeLimitInWeeks")
                        .HasColumnType("integer");

                    b.Property<int>("OfferTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("UserLoginId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IssuedDigitalShareId");

                    b.HasIndex("OfferTypeId");

                    b.HasIndex("UserLoginId");

                    b.ToTable("OfferedShares");
                });

            modelBuilder.Entity("BBS.Models.OfferType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OfferTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Auction"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Private"
                        });
                });

            modelBuilder.Entity("BBS.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("AddressLine")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("AnnualIncome")
                        .HasColumnType("numeric");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfEmployement")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("EmiratesID")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("EmployementTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("EmployerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("HaveConvicted")
                        .HasColumnType("boolean");

                    b.Property<bool>("HaveCriminalRecord")
                        .HasColumnType("boolean");

                    b.Property<bool>("HaveExperience")
                        .HasColumnType("boolean");

                    b.Property<bool>("HavePriorExpirence")
                        .HasColumnType("boolean");

                    b.Property<bool>("HaveTraining")
                        .HasColumnType("boolean");

                    b.Property<string>("IBANNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("IPAddress")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsIndividual")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPublicSectorEmployee")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsUSCitizen")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("ModifiedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("NationalityId")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VaultNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("VerificationState")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("EmployementTypeId");

                    b.HasIndex("NationalityId");

                    b.ToTable("Person");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddedById = 0,
                            AddedDate = new DateTime(2022, 5, 23, 14, 47, 44, 327, DateTimeKind.Local).AddTicks(2450),
                            AddressLine = "Addis Ababa",
                            AnnualIncome = 1000m,
                            City = "Addis Ababa",
                            CountryId = 1,
                            DateOfBirth = new DateTime(2022, 5, 23, 11, 47, 44, 327, DateTimeKind.Utc).AddTicks(2470),
                            DateOfEmployement = new DateTime(2022, 5, 23, 11, 47, 44, 327, DateTimeKind.Utc).AddTicks(2477),
                            Email = "admin@gmail.com",
                            EmiratesID = "000000000000",
                            EmployementTypeId = 1,
                            EmployerName = "None",
                            FirstName = "Admin",
                            HaveConvicted = false,
                            HaveCriminalRecord = false,
                            HaveExperience = false,
                            HavePriorExpirence = false,
                            HaveTraining = false,
                            IBANNumber = "00000000000",
                            IsDeleted = false,
                            IsIndividual = false,
                            IsPublicSectorEmployee = false,
                            IsUSCitizen = false,
                            LastName = "Admin",
                            ModifiedById = 0,
                            ModifiedDate = new DateTime(2022, 5, 23, 14, 47, 44, 327, DateTimeKind.Local).AddTicks(2466),
                            NationalityId = 1,
                            PhoneNumber = "0926849888",
                            VaultNumber = "00000000000",
                            VerificationState = 0
                        });
                });

            modelBuilder.Entity("BBS.Models.PersonalAttachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Back")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Front")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IPAddress")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("ModifiedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonalAttachments");
                });

            modelBuilder.Entity("BBS.Models.Restriction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Restrictions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "To the best of your knwledge, are there any sale or transfer restrictions related to these shares ? Are you a cofounder or employee at the company ? "
                        },
                        new
                        {
                            Id = 2,
                            Name = "Are you a cofounder or employee at the company?"
                        });
                });

            modelBuilder.Entity("BBS.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Investor"
                        });
                });

            modelBuilder.Entity("BBS.Models.Share", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("BusinessLogo")
                        .HasColumnType("text");

                    b.Property<string>("CompanyInformationDocument")
                        .HasColumnType("text");

                    b.Property<string>("CompanyName")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfGrant")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DebtRoundId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int>("EquityRoundId")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<int>("GrantTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("IPAddress")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<int>("ModifiedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("NumberOfShares")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("Restriction1")
                        .HasColumnType("boolean");

                    b.Property<bool>("Restriction2")
                        .HasColumnType("boolean");

                    b.Property<string>("ShareOwnershipDocument")
                        .HasColumnType("text");

                    b.Property<decimal>("SharePrice")
                        .HasColumnType("numeric");

                    b.Property<int>("StorageLocationId")
                        .HasColumnType("integer");

                    b.Property<int>("UserLoginId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserLoginId");

                    b.ToTable("Shares");
                });

            modelBuilder.Entity("BBS.Models.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("States");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pending",
                            Value = 0
                        },
                        new
                        {
                            Id = 2,
                            Name = "Returned",
                            Value = 1
                        },
                        new
                        {
                            Id = 3,
                            Name = "Completed",
                            Value = 2
                        });
                });

            modelBuilder.Entity("BBS.Models.StorageLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("StorageLocations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Dropbox"
                        },
                        new
                        {
                            Id = 2,
                            Name = "One Drive"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Google "
                        },
                        new
                        {
                            Id = 4,
                            Name = "iCloud"
                        },
                        new
                        {
                            Id = 5,
                            Name = "My Desktop"
                        });
                });

            modelBuilder.Entity("BBS.Models.UserLogin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IPAddress")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("ModifiedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Passcode")
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("UserLogin");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddedById = 0,
                            AddedDate = new DateTime(2022, 5, 23, 14, 47, 44, 327, DateTimeKind.Local).AddTicks(2725),
                            IsDeleted = false,
                            ModifiedById = 0,
                            ModifiedDate = new DateTime(2022, 5, 23, 14, 47, 44, 327, DateTimeKind.Local).AddTicks(2727),
                            Passcode = "MTIzNA==",
                            PasswordHash = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            PasswordSalt = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            PersonId = 1,
                            RefreshToken = "",
                            Username = ""
                        });
                });

            modelBuilder.Entity("BBS.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IPAddress")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("ModifiedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("UserLoginId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserLoginId");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddedById = 0,
                            AddedDate = new DateTime(2022, 5, 23, 14, 47, 44, 327, DateTimeKind.Local).AddTicks(2767),
                            IsDeleted = false,
                            ModifiedById = 0,
                            ModifiedDate = new DateTime(2022, 5, 23, 14, 47, 44, 327, DateTimeKind.Local).AddTicks(2768),
                            RoleId = 1,
                            UserLoginId = 1
                        });
                });

            modelBuilder.Entity("BBS.Models.IssuedDigitalShare", b =>
                {
                    b.HasOne("BBS.Models.UserLogin", "UserLogin")
                        .WithMany()
                        .HasForeignKey("UserLoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserLogin");
                });

            modelBuilder.Entity("BBS.Models.OfferedShare", b =>
                {
                    b.HasOne("BBS.Models.IssuedDigitalShare", "IssuedDigitalShare")
                        .WithMany()
                        .HasForeignKey("IssuedDigitalShareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BBS.Models.OfferType", "OfferType")
                        .WithMany()
                        .HasForeignKey("OfferTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BBS.Models.UserLogin", "UserLogin")
                        .WithMany()
                        .HasForeignKey("UserLoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IssuedDigitalShare");

                    b.Navigation("OfferType");

                    b.Navigation("UserLogin");
                });

            modelBuilder.Entity("BBS.Models.Person", b =>
                {
                    b.HasOne("BBS.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BBS.Models.EmployementType", "EmployementType")
                        .WithMany()
                        .HasForeignKey("EmployementTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BBS.Models.Nationality", "Nationality")
                        .WithMany()
                        .HasForeignKey("NationalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("EmployementType");

                    b.Navigation("Nationality");
                });

            modelBuilder.Entity("BBS.Models.PersonalAttachment", b =>
                {
                    b.HasOne("BBS.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BBS.Models.Share", b =>
                {
                    b.HasOne("BBS.Models.UserLogin", "UserLogin")
                        .WithMany()
                        .HasForeignKey("UserLoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserLogin");
                });

            modelBuilder.Entity("BBS.Models.UserLogin", b =>
                {
                    b.HasOne("BBS.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BBS.Models.UserRole", b =>
                {
                    b.HasOne("BBS.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BBS.Models.UserLogin", "UserLogin")
                        .WithMany()
                        .HasForeignKey("UserLoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("UserLogin");
                });
#pragma warning restore 612, 618
        }
    }
}
