using Avion.Areas.Admin.ViewModels.Advert;
using Avion.Areas.Admin.ViewModels.Feature;
using Avion.Areas.Admin.ViewModels.Hero;
using Avion.Areas.Admin.ViewModels.Idea;
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
    }
}
