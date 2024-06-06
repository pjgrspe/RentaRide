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
        public string GenerateReceiptNumber()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string randomLetters = new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            int randomNumber = random.Next(0, 9999);
            string receiptNumber = randomLetters + "-" + randomNumber.ToString("D4");
            return receiptNumber;
        }
    }
}
