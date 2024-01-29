namespace Avion.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<BrandCategory> BrandCategories { get; set; } = new HashSet<BrandCategory>();
        public List<Product> Products { get; set; }

    }
}
