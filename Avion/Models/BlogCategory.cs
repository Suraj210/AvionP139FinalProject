namespace Avion.Models
{
    public class BlogCategory:BaseEntity
    {
        public string Name { get; set; }
        public List<Blog> MyProperty { get; set; }
    }
}
