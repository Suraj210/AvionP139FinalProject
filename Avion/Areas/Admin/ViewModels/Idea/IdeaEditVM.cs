using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.Idea
{
    public class IdeaEditVM
    {
        public int Id { get; set; }
        public string Image { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }

        public IFormFile Photo { get; set; }
    }
}
