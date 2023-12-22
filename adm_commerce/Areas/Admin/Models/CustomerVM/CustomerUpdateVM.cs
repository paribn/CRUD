using System.ComponentModel.DataAnnotations;

namespace adm_commerce.Areas.Admin.Models.CustomerVM
{
    public class CustomerUpdateVM
    {

        [Required(ErrorMessage = "Something went wrong!")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [MinLength(3, ErrorMessage = "Name can't be less than 3 characters!")]
        [MaxLength(10, ErrorMessage = "Name can't be more than 10 characters!")]
        public string? FistName { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [MinLength(5, ErrorMessage = "Name can't be less than 5 characters!")]
        [MaxLength(10, ErrorMessage = "Name can't be more than 10 characters!")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "The email address is required!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
    }
}
