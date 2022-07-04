using BBS.Models;
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
                //optionsBuilder.UseNpgsql("Server=172.16.0.1;Port=5432;Database=Busra_Staging;User Id=postgres;Password=7Xp2NGP45Wux");                
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
        public virtual DbSet<OfferTimeLimit> OfferTimeLimits { get; set; }
        public virtual DbSet<OfferPayment> OfferPayments { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }


        // Bid Shares 
        public virtual DbSet<BidShare> BidShares { get; set; }


        // Investor
        public virtual DbSet<InvestorDetail> InvestorDetails { get; set; }
        public virtual DbSet<InvestorRiskType> InvestorRiskTypes { get; set; }
        public virtual DbSet<InvestorType> InvestorTypes { get; set; }

        // Category
        public virtual DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>().HasData(
               new Category { Id = 1, Name = "Information", Content = "" },
               new Category { Id = 2, Name = "Deal Teaser", Content = "" },
               new Category { Id = 3, Name = "Team", Content = "" },
               new Category { Id = 4, Name = "Interviews", Content = "" },
               new Category { Id = 5, Name = "News", Content = "" }
           );

            modelBuilder.Entity<InvestorType>().HasData(
                new InvestorType { Id = 1, Value = "Retail" },
                new InvestorType { Id = 2, Value = "Qualified" }
            );

            modelBuilder.Entity<InvestorRiskType>().HasData(
                new InvestorRiskType { Id = 1, Value = "High Risk" },
                new InvestorRiskType { Id = 2, Value = "Normal" }
            );

            modelBuilder.Entity<PaymentType>().HasData(
                new PaymentType { Id = 1, Value = "Bank Transfer" },
                new PaymentType { Id = 2, Value = "Debit/Credit Cash" }
            );

            modelBuilder.Entity<OfferTimeLimit>().HasData(
                new OfferTimeLimit { Id = 1, Value = "3 Days" },
                new OfferTimeLimit { Id = 2, Value = "1 Week" },
                new OfferTimeLimit { Id = 3, Value = "3 Months" },
                new OfferTimeLimit { Id = 4, Value = "6 Months" }
            );

            modelBuilder.Entity<State>().HasData(
                 new State { Id = 1, Name = "Pending" },
                 new State { Id = 2, Name = "Completed" },
                 new State { Id = 3, Name = "Returned" }
            );

            modelBuilder.Entity<Role>().HasData(
                 new Role { Id = 2, Name = "Admin" },
                 new Role { Id = 1, Name = "Investor" }
            );

            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "United arab Emirates" },
                new Country { Id = 2, Name = "Pakistan" },
                new Country { Id = 3, Name = "British" }
            );


            modelBuilder.Entity<Nationality>().HasData(
                new Nationality { Id = 1, Name = "Emirati" },
                new Nationality { Id = 2, Name = "Pakistani" },
                new Nationality { Id = 3, Name = "United Kingdom" }
            );

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
                     Name = "Prefered",
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
                new EquityRound { Id = 1, Name = "Pre-Seed" },
                new EquityRound { Id = 2, Name = "Seed" },
                new EquityRound { Id = 3, Name = "Angel" },
                new EquityRound { Id = 4, Name = "Serie A" },
                new EquityRound { Id = 5, Name = "Serie B" },
                new EquityRound { Id = 6, Name = "Serie C" }
            );

            modelBuilder.Entity<DebtRound>().HasData(
                new DebtRound { Id = 1, Name = "Mezzanine" },
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
