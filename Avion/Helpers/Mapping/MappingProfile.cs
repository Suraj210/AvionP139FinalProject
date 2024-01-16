using AutoMapper;
using Avion.Areas.Admin.ViewModels.Advert;
using Avion.Areas.Admin.ViewModels.Feature;
using Avion.Areas.Admin.ViewModels.Hero;
using Avion.Areas.Admin.ViewModels.Idea;
using Avion.Areas.Admin.ViewModels.Testimonial;
using Avion.Models;

namespace Avion.Helpers.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Hero, HeroVM>();
            CreateMap<Advert, AdvertVM>();
            CreateMap<Feature, FeatureVM>();
            CreateMap<Idea, IdeaVM>();
            CreateMap<Testimonial, TestimonialVM>();

        }
    }
}
