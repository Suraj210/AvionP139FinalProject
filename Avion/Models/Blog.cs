namespace Avion.Models
{
    public class Blog:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }

        public ICollection<BlogTag> BlogTags { get; set; }

        public BlogCategory BlogCategory { get; set; }

        public int BlogCategoryId { get; set; }
    }
}
