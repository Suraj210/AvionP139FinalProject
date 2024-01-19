using Avion.Areas.Admin.ViewModels.Contact;

namespace Avion.Services.Interfaces
{
    public interface IContactService
    {
        ContactVM GetData();
        Task CreateAsync(ContactMessageCreateVM vm);
    }
}
