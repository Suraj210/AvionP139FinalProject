using Avion.Areas.Admin.ViewModels.Brand;
using Avion.Areas.Admin.ViewModels.Category;
using Avion.Areas.Admin.ViewModels.Product;
using Avion.Helpers;

namespace Avion.ViewModels
{
    public class ShopPageVM
    {
        public Paginate<ProductVM> PaginatedDatas { get; set; }

        public List<CategoryVM> Categories { get; set; }
        public List<BrandVM> Brands { get; set; }

        public string SearchText { get; set; }
        public string SortText { get; set; }


    }
}
