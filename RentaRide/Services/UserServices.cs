using Microsoft.AspNetCore.Identity;
using RentaRide.Models.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentaRide.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<RentaRideAppUsers> _userManager;
        private readonly SignInManager<RentaRideAppUsers> _signInManager;

        public UserServices(UserManager<RentaRideAppUsers> userManager, SignInManager<RentaRideAppUsers> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public bool IsUserLoggedIn(ClaimsPrincipal userPrincipal)
        {
            return _signInManager.IsSignedIn(userPrincipal);
        }
    }
}
