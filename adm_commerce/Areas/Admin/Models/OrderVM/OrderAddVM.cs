using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace adm_commerce.Areas.Admin.Models.OrderVM
{
    public class OrderAddVM
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CustomerId { get; set; }


        [Required(ErrorMessage = "Creation Date is required!")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [ValidateNever]
        public List<SelectListItem> Product { get; set; }

        [ValidateNever]
        public List<SelectListItem> Customer { get; set; }
    }
}
