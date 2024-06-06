using System.Security.Claims;

namespace RentaRide.Services
{
    public interface IUserServices
    {
        bool IsUserLoggedIn(ClaimsPrincipal userPrincipal);
        public string GenerateReceiptNumber();
    }

}
