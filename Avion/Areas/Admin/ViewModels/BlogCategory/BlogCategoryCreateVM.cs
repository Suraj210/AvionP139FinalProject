using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.BlogCategory
{
    public class BlogCategoryCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
