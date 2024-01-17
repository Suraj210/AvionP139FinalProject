namespace Avion.Models
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection<BrandCategory> BrandCategories { get; set; }
        public List<Product> Products { get; set; }
    }
}
