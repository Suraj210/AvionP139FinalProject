using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.About
{
    public class AboutEditVM
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}
