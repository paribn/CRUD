using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace adm_commerce.Areas.Admin.Models
{
    public class AuthenticationLoginVM
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is invalid")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Pasword is required")]
        [DataType(DataType.Password)]
        [MaxLength(255, ErrorMessage = "First name can't be more than 255 characters")]
        public string Password { get; set; }

        [ValidateNever]
        public string ErrorMessage { get; set; }

        public bool RememberMe { get; set; }
    }
}
