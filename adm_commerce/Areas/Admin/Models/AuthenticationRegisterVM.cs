using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace adm_commerce.Areas.Admin.Models
{
    public class AuthenticationRegisterVM
    {
        [Required(ErrorMessage = "First name is required!")]
        [MaxLength(255, ErrorMessage = "First name can't be more than 255 characters")]
        [MinLength(5, ErrorMessage = "First name  can't be less than 5 characters!")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is invalid")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Pasword is required")]
        [MaxLength(255, ErrorMessage = "First name can't be more than 255 characters")]
        [MinLength(8, ErrorMessage = "First name  can't be less than 8 characters!")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string Password { get; set; }

        [ValidateNever]
        public string ErrorMessage { get; set; }
    }
}
