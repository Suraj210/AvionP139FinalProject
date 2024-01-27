using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.Tag
{
    public class TagCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
