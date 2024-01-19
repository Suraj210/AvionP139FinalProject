namespace Avion.Areas.Admin.ViewModels.About
{
    public class AboutVM
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Heading { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public string Image { get; set; }
    }
}
