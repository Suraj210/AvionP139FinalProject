namespace Avion.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<BrandCategory> BrandCategories { get; set; }
        public List<Product> Products { get; set; }

    }
}
