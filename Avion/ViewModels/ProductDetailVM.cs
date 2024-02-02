using Avion.Areas.Admin.ViewModels.Product;
using Avion.Areas.Admin.ViewModels.Review;
using System;

namespace Avion.ViewModels
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public ProductVM Product { get; set; }
        public List<ProductVM> FeaturedProducts { get; set; }
        public ReviewCreateVM NewReview { get; set; }
        public List<ReviewVM> Reviews { get; set; }

    }
}
