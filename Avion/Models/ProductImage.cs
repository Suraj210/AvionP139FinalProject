namespace Avion.Models
{
    public class ProductImage:BaseEntity
    {
        public bool IsMain { get; set; } = false;
        public string Image { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
