using adm_commerce.Entities;

namespace adm_commerce.Areas.Admin.Models.CustomerVM
{
    public class CustomerIndexVM
    {
        public List<Customer> Customers { get; set; }
        public int PageCount { get; set; }
    }
}
