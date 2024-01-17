namespace Avion.Models
{
    public class BrandCategory
    {
        public int Id { get; set; }
        public Brand Brand { get; set; }
        public int BrandId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
