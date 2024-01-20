using Avion.Areas.Admin.ViewModels.Blog;
using Avion.Areas.Admin.ViewModels.BlogCategory;
using Avion.Areas.Admin.ViewModels.Tag;
using Avion.Helpers;

namespace Avion.ViewModels
{
    public class BlogPageVM
    {
        public Paginate<BlogVM> PaginatedDatas { get; set; }
        public List<TagVM> Tags { get; set; }
        public List<BlogCategoryVM> BlogCategories { get; set; }
    
    }
}
