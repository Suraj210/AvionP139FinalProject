using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.Hero
{
    public class HeroEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}
