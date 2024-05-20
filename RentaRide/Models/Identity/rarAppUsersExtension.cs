using RentaRide.Database.Database_Models;

namespace RentaRide.Models.Identity
{
    public class rarAppUsersExtension : RentaRideAppUsers
    {
        public UserDetailsModel UserDetails { get; set; }
    }
}
