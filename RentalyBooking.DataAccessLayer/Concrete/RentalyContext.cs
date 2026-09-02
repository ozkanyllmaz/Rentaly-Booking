using Microsoft.EntityFrameworkCore;
using RentalyBooking.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.DataAccessLayer.Concrete
{
    public class RentalyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LENOVO\\SQLEXPRESS;Database=RentalyBookingDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // marka silinince modellerin otomatik silinmesini engelle
            modelBuilder.Entity<CarModel>()
                .HasOne(x => x.Brand)
                .WithMany()
                .HasForeignKey(x => x.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            // marka silinince araçların silinmesini engelle
            modelBuilder.Entity<Car>()
                .HasOne(x => x.Brand)
                .WithMany(b => b.Cars)
                .HasForeignKey(x => x.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            // model silinince aracın silinmesini engelle
            modelBuilder.Entity<Car>()
                .HasOne(x => x.CarModel)
                .WithMany()
                .HasForeignKey(x => x.CarModelId)
                .OnDelete(DeleteBehavior.Restrict);

            // branch silince rental silinmesin
            modelBuilder.Entity<Rentaly>()
                .HasOne(x => x.PickupBranch)
                .WithMany()
                .HasForeignKey(x => x.PickupBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // branch silince rental silinmesin
            modelBuilder.Entity<Rentaly>()
                .HasOne(x => x.ReturnBranch)
                .WithMany()
                // Branch içerisinde List<Rental> olsaydı o zaman .WithMany içini doldurmak zorundaydık shadow property olmaması için.
                .HasForeignKey(x => x.ReturnBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            //araç silinince kiralama geçmişi silinmesin
            modelBuilder.Entity<Rentaly>()
                .HasOne(x => x.Car)
                .WithMany(x => x.Rentals)
                .HasForeignKey(x => x.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            //müşteri silinince kiralam geçmişi silinmesin
            modelBuilder.Entity<Rentaly>()
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rentaly> Rentals { get; set; }
        public DbSet<FuelPrice> FuelPrices { get; set; }
        public DbSet<OurFeature> OurFeatures { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<GeneralFeature> GeneralFeatures { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
    }
}
