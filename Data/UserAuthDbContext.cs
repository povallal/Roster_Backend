using System;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using rosterapi.Models;

namespace rosterapi.Data
{
    public class UserAuthDbContext : IdentityDbContext<User, IdentityRole<string>, string>
    {
        public UserAuthDbContext(DbContextOptions<UserAuthDbContext> options) : base(options)
        {
        }

        // Define DbSet for other models if needed
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ChiefConsultant> ChiefConsultants { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<MedicalOfficer> MedicalOfficers { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed roles
            builder.Entity<IdentityRole<string>>().HasData(
                new IdentityRole<string> { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<string> { Id = "2", Name = "ChiefConsultant", NormalizedName = "CHIEFCONSULTANT" },
                new IdentityRole<string> { Id = "3", Name = "Consultant", NormalizedName = "CONSULTANT" },
                new IdentityRole<string> { Id = "4", Name = "MedicalOfficer", NormalizedName = "MEDICALOFFICER" }
            );

            // Define relationships


            // Configure the one-to-one relationship between Unit and ChiefConsultant
            builder.Entity<ChiefConsultant>()
           .HasOne(cc => cc.Unit)
           .WithOne(u => u.ChiefConsultant)
           .HasForeignKey<ChiefConsultant>(cc => cc.UnitId)
           .OnDelete(DeleteBehavior.Restrict);



            // MedicalOfficer-Group relationship
            builder.Entity<MedicalOfficer>()
                .HasOne(mo => mo.Group)
                .WithMany(g => g.MedicalOfficers)
                .HasForeignKey(mo => mo.GroupId)
                .OnDelete(DeleteBehavior.Restrict);


            
        }
    }
}

