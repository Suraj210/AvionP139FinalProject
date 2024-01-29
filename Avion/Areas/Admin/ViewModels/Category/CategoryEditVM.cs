using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.Category
{
    public class CategoryEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public IList<SelectListItem> Brands { get; set; }
    }
}
