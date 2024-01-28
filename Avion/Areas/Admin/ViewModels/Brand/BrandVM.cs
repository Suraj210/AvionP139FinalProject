
namespace Avion.Areas.Admin.ViewModels.Brand
{
    public class BrandVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public DateTime CreateTime { get; set; }
        public List<Avion.Models.Category> Categories { get; set; }
        public bool SoftDeleted { get; set; }

    }
}
