using Avion.Areas.Admin.ViewModels.About;
using Avion.Areas.Admin.ViewModels.Feature;
using Avion.Services.Interfaces;
using Avion.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private readonly IFeatureService _featureService;

        public AboutController(IAboutService aboutService,
                               IFeatureService featureService )
        {
            _aboutService = aboutService;
            _featureService = featureService;
        }
        public async Task<IActionResult> Index()
        {
            List<AboutVM> abouts = await _aboutService.GetAllAsync();
            List<FeatureVM> features = await _featureService.GetAllAsync();


            AboutPageVM model = new()
            {
                Abouts = abouts,
                Features = features

            };

            return View( model );
        }
    }
}
