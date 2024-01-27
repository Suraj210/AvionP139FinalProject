using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.BlogCategory
{
    public class BlogCategoryEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
