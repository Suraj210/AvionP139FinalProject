using Avion.Areas.Admin.ViewModels.Contact;

namespace Avion.ViewModels
{
    public class ContactPageVM
    {
        public ContactVM Contact { get; set; }
        public ContactMessageCreateVM NewContact { get; set; }
    }
}
