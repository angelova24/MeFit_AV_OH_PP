using MeFit.DAL.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace MeFit.DAL.Models.Data
{
    public class MeFitDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }

        public MeFitDbContext(DbContextOptions<MeFitDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            
            //User
            modelBuilder.Entity<User>()            
            .HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<Profile>(p => p.UserId);

            //User seeding
            modelBuilder.Entity<User>()
             .HasData(new User()
             {
                 Id = 1,
                 FirstName = "John",
                 LastName = "Doe",
                 Password = "password1234",
                 IsAdmin = false,
                 Username = "Doe.J",
                 IsContributor = false,
                 ProfileId = 1
             });

            //Address
            modelBuilder.Entity<Address>()           
           .HasOne(a => a.Profile)
           .WithOne(p => p.Address)
           .HasForeignKey<Profile>(p => p.AddressId);

            //Profile seeding
            modelBuilder.Entity<Profile>()
            .HasData(new Profile { Id = 1, UserId = 1, Disabilities = "none", Height = 1.70, MedicalConditions = "healthy", Weight = 78.05 });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
