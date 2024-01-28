using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.Blog
{
    public class BlogCreateVM
    {

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile Photo { get; set; }

        public IList<SelectListItem> Tags { get; set; }
        public int BlogCategoryId { get; set; }

    }
}
