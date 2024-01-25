using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.Testimonial
{
    public class TestiimonialCreateVM
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
