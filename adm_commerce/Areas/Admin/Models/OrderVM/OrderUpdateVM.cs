using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace adm_commerce.Areas.Admin.Models.OrderVM
{
    public class OrderUpdateVM
    {

        [Required(ErrorMessage = "Something went wrong!")]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CustomerId { get; set; }


        [Required(ErrorMessage = "Creation Date is required!")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [Required]
        public List<SelectListItem> Product { get; set; }

        [Required]
        public List<SelectListItem> Customer { get; set; }
    }
}
