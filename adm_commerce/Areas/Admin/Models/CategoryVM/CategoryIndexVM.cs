using adm_commerce.Entities;

namespace adm_commerce.Areas.Admin.Models.CategoryVM
{
    public class CategoryIndexVM
    {
        public List<Category> Categories { get; set; }

        public int PageCount { get; set; }
    }
}
