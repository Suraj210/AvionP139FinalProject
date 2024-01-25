using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.Privacy
{
    public class PrivacyEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Context { get; set; }
    }
}
