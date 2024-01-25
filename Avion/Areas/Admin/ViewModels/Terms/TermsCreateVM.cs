using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.Terms
{
    public class TermsCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Context { get; set; }
    }
}
