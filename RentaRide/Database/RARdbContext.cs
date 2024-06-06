using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentaRide.Database.Database_Models;
using RentaRide.Models.Identity;
using System.Reflection.Emit;

namespace RentaRide.Database
{
    public class RARdbContext : IdentityDbContext<RentaRideAppUsers>
    {

        public RARdbContext(DbContextOptions<RARdbContext> options) : base(options)
        {

        }

        //public DbSet<RentaRideAppUsers> TBL_RentaRideAppUsers { get; set; }
        public DbSet<UserDetailsDBModel> TBL_UserDetails { get; set; }
        public DbSet<DriversDBModel> TBL_Drivers { get; set; }
        public DbSet<CarsDBModel> TBL_Cars { get; set; }
        public DbSet<CarImagesDBModel> TBL_CarImages { get; set; }
        public DbSet<CarTypesDBModel> TBL_CarTypes { get; set; }
        public DbSet<CarLogsDBModel> TBL_CarLogs { get; set; }
        public DbSet<ListingsDBModel> TBL_Listings { get; set; }
        public DbSet<OrdersDBModel> TBL_Orders { get; set; }
        public DbSet<PayTypesDBModel> TBL_PayTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserDetailsDBModel>()
                .HasOne(ud => ud.RentaRideAppUsers)
                .WithOne()
                .HasForeignKey<UserDetailsDBModel>(ud => ud.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CarImagesDBModel>()
                .HasOne(p => p.carsDBModel)
                .WithMany()
                .HasForeignKey(p => p.carID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ListingsDBModel>()
                .Property(l => l.listingHourlyPrice)
                .HasPrecision(9, 2);

            builder.Entity<ListingsDBModel>()
                .Property(l => l.listingDailyPrice)
                .HasPrecision(9, 2);

            builder.Entity<ListingsDBModel>()
                .Property(l => l.listingWeeklyPrice)
                .HasPrecision(9, 2);

            builder.Entity<ListingsDBModel>()
                .Property(l => l.listingMonthlyPrice)
                .HasPrecision(9, 2);

            builder.Entity<OrdersDBModel>()
                .Property(o => o.orderTotalCost)
                .HasPrecision(9, 2);

            builder.Entity<OrdersDBModel>()
                .Property(o => o.orderExtraFees)
                .HasPrecision(9, 2);
        }
    }
}
