namespace adm_commerce.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; }

        public int ProductId { get; set; }  
        public virtual Product Product { get; set; }


       
    }
}
