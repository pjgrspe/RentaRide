using Microsoft.AspNetCore.Identity;
using RentaRide.Database.Database_Models;
using System.ComponentModel.DataAnnotations;

namespace RentaRide.Models.Identity
{
    public class RentaRideAppUsers : IdentityUser
    {
        [Required]
        public string userFirstName { get; set; }
        public string? userMiddleName { get; set; }
        [Required]
        public string userLastName { get; set; }
        [Required]
        public bool userisApproved { get; set; }
        [Required]
        public bool userisActive { get; set; } = false;
    }
}
