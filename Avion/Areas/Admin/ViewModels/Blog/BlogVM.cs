using Avion.Models;

namespace Avion.Areas.Admin.ViewModels.Blog
{
    public class BlogVM
    {
        public DateTime CreateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }

        public ICollection<Avion.Models.Tag> Tags { get; set; }

        public string BlogCategoryName { get; set; }

        public int BlogCategoryId { get; set; }
    }
}
