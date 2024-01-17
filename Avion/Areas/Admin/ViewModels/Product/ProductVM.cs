using Avion.Models;

namespace Avion.Areas.Admin.ViewModels.Product
{
    public class ProductVM
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public string Image { get; set; }

        public List<ProductImage> Images { get; set; }

        public string Material { get; set; }

        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public decimal Length { get; set; }

        public string BrandName { get; set; }
        public int BrandId { get; set; }

        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
