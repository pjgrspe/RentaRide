using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RentaRide.Models.Identity
{
    public class RentaRideAppUsers : IdentityUser
    {
        [Required]
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        public string? LastName { get; set; }
    }
}
