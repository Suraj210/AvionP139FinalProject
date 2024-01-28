using Avion.Models;

namespace Avion.Areas.Admin.ViewModels.Blog
{
    public class BlogVM
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }

        public List<Avion.Models.Tag> Tags { get; set; }

        public string BlogCategoryName { get; set; }

        public int BlogCategoryId { get; set; }

        public bool SoftDeleted { get; set; }
    }
}
