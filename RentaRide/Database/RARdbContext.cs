using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentaRide.Database.Database_Models;
using RentaRide.Models.Identity;

namespace RentaRide.Database
{
    public class RARdbContext : IdentityDbContext
    {
        public RARdbContext()
        {

        }

        public RARdbContext(DbContextOptions<RARdbContext> options) : base(options)
        {

        }

        public DbSet<RentaRideAppUsers> TBL_RentaRideAppUsers { get; set; }
        public DbSet<UserDetailsModel> TBL_UserDetails { get; set; }
    }
}
