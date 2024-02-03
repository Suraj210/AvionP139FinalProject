using Avion.Areas.Admin.ViewModels.Contact;

namespace Avion.Services.Interfaces
{
    public interface IContactService
    {
        ContactVM GetData();
        Task CreateAsync(ContactMessageCreateVM vm);
        Task DeleteAsync(int id);
        Task<List<ContactMessageVM>> GetAllMessagesAsync();
        Task<ContactMessageVM> GetMessageByIdAsync(int id);
    }
}
