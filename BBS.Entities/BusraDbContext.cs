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

        public virtual DbSet<CertificateType> CertificateTypes { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<PersonalAttachment> PersonalAttachments { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLogin>()
                .HasIndex(b => b.Username)
                .IsUnique();

            modelBuilder.Entity<Role>().HasData(
                 new Role {Id=1, Name = "Investor" });

            modelBuilder.Entity<Country>().HasData(
             new Country { Id = 1, Name = "India" }, new Country { Id = 2, Name = "UAE" });
            
            modelBuilder.Entity<Nationality>().HasData(
             new Nationality { Id = 1, Name = "Indian" }, new Nationality { Id = 2, Name = "Emirati" });
             

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }      
}
