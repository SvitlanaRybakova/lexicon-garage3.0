﻿using lexicon_garage3.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lexicon_garage3.Persistance.Data
{
    public class ApplicationDbContext : IdentityDbContext<Member, IdentityRole, string>
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
            modelBuilder.Entity<ParkingSpot>()
             .HasOne(p => p.Vehicle) 
             .WithOne(v => v.ParkingSpot)
             .HasForeignKey<ParkingSpot>(p => p.RegNumber)
             .IsRequired(false); // Make the foreign key optional

            //vehicletype stores the enum name ("Small", "Medium", "Large") as a string
            modelBuilder.Entity<VehicleType>()
           .Property(v => v.VehicleSize)
           .HasConversion<string>(); 

           
            //modelBuilder.Entity<VehicleType>().HasData(
            //    new VehicleType { Id = 1, VehicleTypeName = "Car", NumOfWheels = 4, VehicleSize = "Medium" },
            //    new VehicleType { Id = 2, VehicleTypeName = "Motorcycle", NumOfWheels = 2, VehicleSize = "Small" },
            //    new VehicleType { Id = 3, VehicleTypeName = "Truck", NumOfWheels = 6, VehicleSize = "Large" }
            //);

            
            //modelBuilder.Entity<Member>().HasData(
            //    new Member { Id = "M001", FirstName = "Anna", LastName = "Darke", PersonNumber = "19870324", UserName = "annadark" },
            //    new Member { Id = "M002", FirstName = "Jane", LastName = "Austin", PersonNumber = "19960712", UserName = "janeaustin" }
            //);

          
            //modelBuilder.Entity<ParkingSpot>().HasData(
            //    new ParkingSpot { Id = "P001", ParkingNumber = 1, HourRate = 10, IsAvailable = true, Size = "Medium", RegNumber = "CAR001" },
            //    new ParkingSpot { Id = "P002", ParkingNumber = 2, HourRate = 15, IsAvailable = true, Size = "Small" , RegNumber = "BIKE001" }
            //);

    
            //modelBuilder.Entity<Vehicle>().HasData(
            //    new Vehicle
            //    {
            //        RegNumber = "CAR001",
            //        Color = "Red",
            //        Brand = "Toyota",
            //        Model = "Corolla",
            //        ArrivalTime = DateTime.Now.AddHours(-2),
            //        CheckoutTime = DateTime.Now.AddHours(2),
            //        VehicleTypeId = 1 
            //    },
            //    new Vehicle
            //    {
            //        RegNumber = "BIKE001",
            //        Color = "Black",
            //        Brand = "Yamaha",
            //        Model = "MT-15",
            //        ArrivalTime = DateTime.Now.AddHours(-1),
            //        CheckoutTime = DateTime.Now.AddHours(3),
            //        VehicleTypeId = 2 
            //    }
            //);

        }
        public DbSet<Member> Member { get; set; } = default!;
        public DbSet<ParkingSpot> ParkingSpot { get; set; } = default!;
        public DbSet<Vehicle> Vehicle { get; set; } = default!;
        public DbSet<VehicleType> VehicleType { get; set; } = default!;
    }
}
