﻿using Avion.Areas.Admin.ViewModels.Advert;
using Avion.Areas.Admin.ViewModels.Blog;
using Avion.Areas.Admin.ViewModels.Brand;
using Avion.Areas.Admin.ViewModels.Feature;
using Avion.Areas.Admin.ViewModels.Hero;
using Avion.Areas.Admin.ViewModels.Idea;
using Avion.Areas.Admin.ViewModels.Product;
using Avion.Areas.Admin.ViewModels.Testimonial;
using Avion.Services.Interfaces;
using Avion.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHeroService _heroService;
        private readonly IAdvertService _advertService;
        private readonly IFeatureService _featureService;
        private readonly IIdeaService _ideaService;
        private readonly ITestimonialService _testimonialService;
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly IBlogService _blogService;

        public HomeController(IHeroService heroService,
                              IAdvertService advertService,
                              IFeatureService featureService,
                              IIdeaService ideaService,
                              ITestimonialService testimonialService,
                              IProductService productService,
                              IBrandService brandService,
                              IBlogService blogService)
        {
            _heroService = heroService;
            _advertService = advertService;
            _featureService = featureService;
            _ideaService = ideaService;
            _testimonialService = testimonialService;
            _productService = productService;
            _brandService = brandService;
            _blogService = blogService;

        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<HeroVM> heroes = await _heroService.GetAllAsync();
            List<AdvertVM> adverts = await _advertService.GetAllAsync();
            List<FeatureVM> features = await _featureService.GetAllAsync();
            IdeaVM idea = await _ideaService.GetAsync();
            List<TestimonialVM> testimonials = await _testimonialService.GetAllAsync();
            List<ProductVM> products = await _productService.GetAllByTakeAsync(8);
            List<BrandVM> brands = await _brandService.GetAllAsync();
            List<BlogVM> blogs = await _blogService.GetAllByTakeAsync(3);

            HomeVM model = new()
            {
                Heroes = heroes,
                Adverts = adverts,
                Features = features,
                Idea = idea,
                Testimonials = testimonials,
                Products = products,
                Brands = brands,
                Blogs = blogs,
            };

            return View(model);
        }

        
    }
}