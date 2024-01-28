using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.Blog
{
    public class BlogEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile Photo { get; set; }

        public IList<Avion.Models.Tag> Tags { get; set; }
        public int BlogCategoryId { get; set; }
    }
}
