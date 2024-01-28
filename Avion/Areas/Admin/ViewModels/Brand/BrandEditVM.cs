using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.Brand
{
    public class BrandEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Image { get; set; }

        public IFormFile Photo { get; set; }

        public IList<SelectListItem> Categories { get; set; }

    }
}
