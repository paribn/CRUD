using adm_commerce.Entities;

namespace adm_commerce.Models
{
    public class HomeIndexVM
    {
        public List<Product> Products { get; set; }

        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
