using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace adm_commerce.Models
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


        public List<SelectListItem>Product { get; set; }
        public List<SelectListItem>Customer { get; set; }
    }
}
