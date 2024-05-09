using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentaRide.Models.Accounts
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is Required")]
        [DisplayName("Email/Username")]
        public string loginmodelUsername { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string loginmodelPassword { get; set; }
        [DisplayName("Remember Me")]
        public bool loginmodelRememberMe { get; set; }
    }
}
