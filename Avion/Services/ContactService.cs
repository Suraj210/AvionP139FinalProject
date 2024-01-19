using AutoMapper;
using Avion.Areas.Admin.ViewModels.Contact;
using Avion.Data;
using Avion.Models;
using Avion.Services.Interfaces;
using System.Security.Cryptography.Pkcs;

namespace Avion.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;
        private readonly ISettingService _settingService;
        private readonly IMapper _mapper;

        public ContactService(AppDbContext context,
                              ISettingService settingService,
                              IMapper mapper)
        {
            _context = context;
            _settingService = settingService;
            _mapper = mapper;
        }

        public ContactVM GetData()
        {


            Dictionary<string, string> settingDatas =  _settingService.GetSettings();

            ContactVM model = new()
            {
                Email = settingDatas["Mail"],
                Phone = settingDatas["Phone"],
                Address = settingDatas["Address"]
            };

            return model;
        }

        public async Task CreateAsync(ContactMessageCreateVM contact)
        {
            var data = _mapper.Map<ContactMessage>(contact);
            await _context.ContactMessages.AddAsync(data);
            await _context.SaveChangesAsync();

        }
    }
}
