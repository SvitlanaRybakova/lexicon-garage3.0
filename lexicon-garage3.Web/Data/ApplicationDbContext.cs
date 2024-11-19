﻿using lexicon_garage3.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Security.Cryptography;

namespace lexicon_garage3.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>().HasKey(t => t.RegNumber);


            // generated by chatGPT
            // The Principal Side: Vehicle is the principal.The Dependent Side: ParkingSpot is the dependent and holds the foreign key(RegNumber)
            modelBuilder.Entity<Vehicle>()
                   .HasOne(v => v.ParkingSpot)
                   .WithOne(p => p.Vehicle)
                   .HasForeignKey<ParkingSpot>(p => p.RegNumber);


        }
        public DbSet<lexicon_garage3.Core.Entities.Member> Member { get; set; } = default!;
        public DbSet<lexicon_garage3.Core.Entities.ParkingSpot> ParkingSpot { get; set; } = default!;
        public DbSet<lexicon_garage3.Core.Entities.Vehicle> Vehicle { get; set; } = default!;
        public DbSet<lexicon_garage3.Core.Entities.VehicleType> VehicleType { get; set; } = default!;
    }
}
