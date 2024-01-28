namespace Avion.Models
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection<BrandCategory> BrandCategories { get; set; } = new HashSet<BrandCategory>();
        public List<Product> Products { get; set; }
    }
}
