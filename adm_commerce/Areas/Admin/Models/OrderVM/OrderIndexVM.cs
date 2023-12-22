using adm_commerce.Entities;

namespace adm_commerce.Areas.Admin.Models.OrderVM
{
    public class OrderIndexVM
    {

        public List<Order> Orders { get; set; }
        public int PageCount { get; set; }
    }
}
