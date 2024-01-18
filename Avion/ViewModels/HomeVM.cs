using Avion.Areas.Admin.ViewModels.Advert;
using Avion.Areas.Admin.ViewModels.Blog;
using Avion.Areas.Admin.ViewModels.Brand;
using Avion.Areas.Admin.ViewModels.Feature;
using Avion.Areas.Admin.ViewModels.Hero;
using Avion.Areas.Admin.ViewModels.Idea;
using Avion.Areas.Admin.ViewModels.Product;
using Avion.Areas.Admin.ViewModels.Subscribe;
using Avion.Areas.Admin.ViewModels.Testimonial;

namespace Avion.ViewModels
{
    public class HomeVM
    {
        public List<HeroVM> Heroes { get; set; }
        public List<AdvertVM> Adverts { get; set; }
        public List<FeatureVM> Features { get; set; }
        public IdeaVM  Idea { get; set; }
        public List<TestimonialVM> Testimonials { get; set; }
        public List<ProductVM> Products { get; set; }
        public List<BrandVM> Brands { get; set; }
        public List<BlogVM> Blogs { get; set; }
        public SubscribeCreateVM Subscribe { get; set; }
    }
}
