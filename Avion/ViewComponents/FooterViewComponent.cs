using Avion.Areas.Admin.ViewModels.Layout;
using Microsoft.AspNetCore.Mvc;

namespace Avion.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            FooterVM model = new FooterVM();
            return await Task.FromResult(View(model));
        }
    }
}
