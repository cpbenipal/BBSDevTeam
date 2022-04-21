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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=Busra_Dev;User Id=postgres;Password=secret");
            }
        }

        // OnBoarding
        public virtual DbSet<CertificateType> CertificateTypes { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<Attachment> PersonalAttachments { get; set; }

        // Register Share
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<EquityRound> EquityRounds { get; set; }
        public virtual DbSet<GrantType> GrantTypes { get; set; }
        public virtual DbSet<DebtRound> DebtRounds { get; set; }
        public virtual DbSet<Restriction> Restrictions { get; set; }
        public virtual DbSet<StorageLocation> StorageLocations { get; set; }
        public virtual DbSet<Share> Shares { get; set; }
        public virtual DbSet<State> States { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>().HasData(
                 new State { Id = 1, Name = "Pending", Value = 0 },
                 new State { Id = 2, Name = "Returned", Value = 1 },
                 new State { Id = 3, Name = "Completed", Value = 2 }
            );

            modelBuilder.Entity<Role>().HasData(
                 new Role { Id = 1, Name = "Investor" });

            modelBuilder.Entity<Country>().HasData(
             new Country { Id = 1, Name = "India" }, new Country { Id = 2, Name = "UAE" });


            modelBuilder.Entity<Nationality>().HasData(
                new Nationality { Id = 1, Name = "Indian" }, new Nationality { Id = 2, Name = "Emirati" });

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

            modelBuilder.Entity<StorageLocation>().HasData(
                  new StorageLocation { Id = 1, Name = "Dropbox" },
                  new StorageLocation { Id = 2, Name = "One Drive" },
                  new StorageLocation { Id = 3, Name = "Google " },
                  new StorageLocation { Id = 4, Name = "iCloud" }
            );

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
