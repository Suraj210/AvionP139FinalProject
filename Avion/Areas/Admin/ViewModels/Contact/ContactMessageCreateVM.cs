using System.ComponentModel.DataAnnotations;

namespace Avion.Areas.Admin.ViewModels.Contact
{
    public class ContactMessageCreateVM
    {
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
