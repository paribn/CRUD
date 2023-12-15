using System.ComponentModel.DataAnnotations;

namespace adm_commerce.Models
{
    public class CategoryAddVM
    {
        [Required(ErrorMessage = "Name is required!")]
        [MinLength(5, ErrorMessage = "Name can't be less than 5 characters!")]
        [MaxLength(10, ErrorMessage = "Name can't be more than 10 characters!")]
        public string? Name { get; set; }

    }
}
