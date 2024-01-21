using Avion.Areas.Admin.ViewModels.Product;

namespace Avion.ViewModels
{
    public class ProductDetailVM
    {
        public ProductVM Product { get; set; }
        public List<ProductVM> FeaturedProducts { get; set; }

    }
}
