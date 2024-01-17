namespace Avion.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public List<ProductImage> Images { get; set; }

        public string Material { get; set; }

        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }

        public Brand Brand { get; set; }
        public int BrandId { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
