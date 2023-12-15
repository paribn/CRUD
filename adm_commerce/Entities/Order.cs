﻿namespace adm_commerce.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;



        // navigation properties
        public Product Product { get; set; }
        public Customer Customer { get; set; }

    }
}
