using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.Testimonial
{
    public class TestimonialEditVM
    {
        public int Id { get; set; }
        public string Image { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Message { get; set; }
        public bool IsMain { get; set; }
        public DateTime CreateTime { get; set; }

        public IFormFile Photo { get; set; }
    }
}
