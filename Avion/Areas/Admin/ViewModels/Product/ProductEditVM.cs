using Avion.Models;
using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.Product
{
    public class ProductEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public List<IFormFile> Photos { get; set; }

        public List<ProductImage> Images { get; set; }
        public string Material { get; set; }

        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public decimal Length { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }
    }
}
