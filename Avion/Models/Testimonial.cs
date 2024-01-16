namespace Avion.Models
{
    public class Testimonial:BaseEntity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public bool IsMain { get; set; } = false;
    }
}
