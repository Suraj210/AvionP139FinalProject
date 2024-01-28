namespace Avion.Models
{
    public class Blog:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }

        public ICollection<BlogTag> BlogTags { get; set; } = new HashSet<BlogTag>();

        public BlogCategory BlogCategory { get; set; }

        public int BlogCategoryId { get; set; }
    }
}
