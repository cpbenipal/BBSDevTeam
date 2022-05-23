﻿using BBS.Models;
using Microsoft.EntityFrameworkCore;

namespace BBS.Entities
{
    public partial class BusraDbContext : DbContext
    {
        public BusraDbContext()
        {
        }
        public BusraDbContext(DbContextOptions<BusraDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=Busra_Dev;User Id=postgres;Password=secret");
            }
        }

        // OnBoarding
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<PersonalAttachment> PersonalAttachments { get; set; }
        public virtual DbSet<EmployementType> EmployementTypes { get; set; }

        // Register Share
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<EquityRound> EquityRounds { get; set; }
        public virtual DbSet<GrantType> GrantTypes { get; set; }
        public virtual DbSet<DebtRound> DebtRounds { get; set; }
        public virtual DbSet<Restriction> Restrictions { get; set; }
        public virtual DbSet<StorageLocation> StorageLocations { get; set; }
        public virtual DbSet<Share> Shares { get; set; }
        public virtual DbSet<State> States { get; set; }

        // Issuing Shares
        public virtual DbSet<IssuedDigitalShare> IssuedDigitalShares { get; set; }


        // Offer Share
        public virtual DbSet<OfferedShare> OfferedShares { get; set; }
        public virtual DbSet<OfferType> OfferTypes { get; set; }
        //public virtual DbSet<NlogDBLog> NlogDBLogs { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                 new Person
                 {
                     FirstName = "Admin",
                     LastName = "Admin",
                     Email = "admin@gmail.com",
                     DateOfBirth = DateTime.UtcNow,
                     IsUSCitizen = false,
                     IsPublicSectorEmployee = false,
                     IsIndividual = false,
                     HaveCriminalRecord = false,
                     HaveConvicted = false,
                     City = "Addis Ababa",
                     AddressLine = "Addis Ababa",
                     EmiratesID = "000000000000",
                     VaultNumber = "00000000000",
                     IBANNumber = "00000000000",
                     EmployerName = "None",
                     AnnualIncome = 1000,
                     DateOfEmployement = DateTime.UtcNow,
                     HavePriorExpirence = false,
                     HaveTraining = false,
                     HaveExperience = false,
                     VerificationState = 1,
                     CountryId = 1,
                     NationalityId = 1,
                     Id = 1,
                     EmployementTypeId = 1,
                     PhoneNumber = "0926849888"
                 }

            );

            modelBuilder.Entity<UserLogin>().HasData(
                 new UserLogin
                 {
                     Id = 1,
                     Passcode = "MTIzNA==",
                     PasswordHash = new byte[32],
                     PasswordSalt = new byte[32],
                     PersonId = 1,
                     Username = "",
                     RefreshToken = "",
                 }

            );

            modelBuilder.Entity<UserRole>().HasData(
                 new UserRole
                 {
                     Id = 1,
                     RoleId = 1,
                     UserLoginId = 1,
                 }

            );

            modelBuilder.Entity<State>().HasData(
                 new State { Id = 1, Name = "Pending", Value = 0 },
                 new State { Id = 2, Name = "Returned", Value = 1 },
                 new State { Id = 3, Name = "Completed", Value = 2 }
            );

            modelBuilder.Entity<Role>().HasData(
                 new Role { Id = 1, Name = "Admin" },
                 new Role { Id = 2, Name = "Investor" });

            modelBuilder.Entity<Country>().HasData(
             new Country { Id = 1, Name = "India" }, new Country { Id = 2, Name = "UAE" });


            modelBuilder.Entity<Nationality>().HasData(
                new Nationality { Id = 1, Name = "Indian" }, new Nationality { Id = 2, Name = "Emirati" });


            modelBuilder.Entity<EmployementType>().HasData(
                new EmployementType { Id = 1, Name = "Employed" }, 
                new EmployementType { Id = 2, Name = "Unemployed" },
                new EmployementType { Id = 3, Name = "Full-Time" },
                new EmployementType { Id = 4, Name = "Part-Time" }
            );


            modelBuilder.Entity<GrantType>().HasData(
                 new GrantType
                 {
                     Id = 1,
                     Name = "Common",
                     Description = "Normal Shares for ownership, voting, and share price appreciation"
                 },
                 new GrantType
                 {
                     Id = 2,
                     Name = "Preffered",
                     Description = "Hybrid shares for ownership, non-voting, " +
                     "but its shareholdres and paid dividends prior to other shareholders"
                 },
                 new GrantType
                 {
                     Id = 3,
                     Name = "Debt",
                     Description = "An obligation to offer dividend or interest which typically " +
                     "includes a promise to convert to equity. " +
                     "Debt holdres get paid back before any other shareholder"
                 }
            );

            modelBuilder.Entity<EquityRound>().HasData(
                new EquityRound { Id = 1, Name = "Angel" }, 
                new EquityRound { Id = 2, Name = "Seed" },
                new EquityRound { Id = 3, Name = "Pre-Seed" },
                new EquityRound { Id = 4, Name = "Serie A" },
                new EquityRound { Id = 5, Name = "Serie B" },
                new EquityRound { Id = 6, Name = "Serie C" }
            );

            modelBuilder.Entity<DebtRound>().HasData(
                new DebtRound { Id = 1, Name = "Mezanine" },
                new DebtRound { Id = 2, Name = "Growth" }
            );
            modelBuilder.Entity<Restriction>().HasData(
               new Restriction { Id = 1, Name = "To the best of your knwledge, are there any sale or transfer restrictions related to these shares ? Are you a cofounder or employee at the company ? " },
               new Restriction { Id = 2, Name = "Are you a cofounder or employee at the company?" }
           );
            modelBuilder.Entity<StorageLocation>().HasData(
                  new StorageLocation { Id = 1, Name = "Dropbox" },
                  new StorageLocation { Id = 2, Name = "One Drive" },
                  new StorageLocation { Id = 3, Name = "Google " },
                  new StorageLocation { Id = 4, Name = "iCloud" },
                  new StorageLocation { Id = 5, Name = "My Desktop" }
            );

            modelBuilder.Entity<OfferType>().HasData(
                  new OfferType { Id = 1, Name = "Auction" },
                  new OfferType { Id = 2, Name = "Private" }
            );
            
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
