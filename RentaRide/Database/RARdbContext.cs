using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentaRide.Database.Database_Models;
using RentaRide.Models.Identity;

namespace RentaRide.Database
{
    public class RARdbContext : IdentityDbContext<RentaRideAppUsers>
    {

        public RARdbContext(DbContextOptions<RARdbContext> options) : base(options)
        {

        }

        //public DbSet<RentaRideAppUsers> TBL_RentaRideAppUsers { get; set; }
        public DbSet<UserDetailsModel> TBL_UserDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserDetailsModel>()
                .HasOne(ud => ud.RentaRideAppUsers)
                .WithOne()
                .HasForeignKey<UserDetailsModel>(ud => ud.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
