namespace Avion.Areas.Admin.ViewModels.Category
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public List<Avion.Models.Brand> Brands { get; set; }
        public bool SoftDeleted { get; set; }
    }
}
