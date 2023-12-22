using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace adm_commerce.Areas.Admin.Models
{
    public class ProductUpdateVM
    {

        [Required(ErrorMessage = "Something went wrong!")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [MinLength(5, ErrorMessage = "Name can't be less than 5 characters!")]
        [MaxLength(10, ErrorMessage = "Name can't be more than 10 characters!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price is required!")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Price can't be less than 0!")]
        public decimal? Price { get; set; }

        [ValidateNever]
        public string ImageName { get; set; }

        [ValidateNever]
        public IFormFile Photo { get; set; }

        public int ProductCategoryId { get; set; }

        [ValidateNever]
        public List<SelectListItem> Category { get; set; }
    }
}
