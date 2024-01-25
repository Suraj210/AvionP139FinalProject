using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.Privacy
{
    public class PrivacyCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Context { get; set; }
    }
}
