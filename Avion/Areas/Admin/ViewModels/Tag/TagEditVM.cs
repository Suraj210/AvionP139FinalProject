using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.Tag
{
    public class TagEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
