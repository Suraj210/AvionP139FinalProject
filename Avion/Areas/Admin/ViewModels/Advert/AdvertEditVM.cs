namespace Avion.Areas.Admin.ViewModels.Advert
{
    public class AdvertEditVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
