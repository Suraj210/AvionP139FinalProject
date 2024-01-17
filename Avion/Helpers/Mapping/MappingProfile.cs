﻿using AutoMapper;
using Avion.Areas.Admin.ViewModels.Advert;
using Avion.Areas.Admin.ViewModels.Brand;
using Avion.Areas.Admin.ViewModels.Category;
using Avion.Areas.Admin.ViewModels.Feature;
using Avion.Areas.Admin.ViewModels.Hero;
using Avion.Areas.Admin.ViewModels.Idea;
using Avion.Areas.Admin.ViewModels.Product;
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

            CreateMap<Product, ProductVM>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                                           .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                                           .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(m => m.IsMain).Image));

            CreateMap<Category, CategoryVM>();
            CreateMap<Brand, BrandVM>();

        }
    }
}
