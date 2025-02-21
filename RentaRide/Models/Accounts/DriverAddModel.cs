﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentaRide.Models.Accounts
{
    public class DriverAddModel
    {
        [Required]
        [DisplayName("First Name")]
        public string drivmodelFirstName { get; set; }
        [Required]
        [DisplayName("Middle Name")]
        public string drivmodelMiddleName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string drivmodelLastName { get; set; }
        [Required]
        [DisplayName("Email")]
        public string drivmodelEmail { get; set; }
        [Required]
        [DisplayName("Contact")]
        public string drivmodelContact { get; set; }
        [Required]
        [DisplayName("Driver Picture")]
        public IFormFile drivmodelImage { get; set; }
        [DisplayName("Driver's License (Front)")]
        [Required]   
        public IFormFile drivmodelLicense { get; set; }
        [Required]
        [DisplayName("Driver's License (Back)")]
        public IFormFile drivmodelLicenseBack { get; set; }
    }
}
