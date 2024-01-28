using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using rosterapi.Models;

namespace rosterapi.Authentication
{
    public class ApplicationDbContext : IdentityDbContext<AdminRegisterModel>


    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
           new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
           new IdentityRole { Name = "ChiefConsultant", NormalizedName = "CHIEFCONSULTANT" },
           new IdentityRole { Name = "Consultant", NormalizedName = "CONSULTANT" },
           new IdentityRole { Name = "MedicalOfficer", NormalizedName = "MEDICALOFFICER" }
           );

        }
    }

}







