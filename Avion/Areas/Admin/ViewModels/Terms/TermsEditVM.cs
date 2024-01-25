using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.Terms
{
    public class TermsEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Context { get; set; }
    }
}
