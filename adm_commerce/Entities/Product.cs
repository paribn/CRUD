﻿namespace adm_commerce.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public ProductImage? ProductImage { get; set; }
        public List<Order> Orders { get; set; }
        public Category Category { get; set; }


    }
}
