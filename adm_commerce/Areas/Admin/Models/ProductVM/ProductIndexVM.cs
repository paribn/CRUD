using adm_commerce.Entities;

namespace adm_commerce.Areas.Admin.Models

{
    public class ProductIndexVM
    {

        public List<Product> Products { get; set; }

        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
