using Avion.Areas.Admin.ViewModels.Layout;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Avion.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public HeaderViewComponent(ILayoutService layoutService, IHttpContextAccessor httpContextAccessor,
           UserManager<AppUser> userManager)
        {
            _layoutService = layoutService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HeaderVM model = _layoutService.GetHeaderDatas();

            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is not null)
            {
                AppUser currentUser = await _userManager.FindByIdAsync(userId);
                model.UserFullName = currentUser.FullName;
                model.UserId = currentUser.Id;
            }


            return await Task.FromResult(View(model));
        }
    }
}
