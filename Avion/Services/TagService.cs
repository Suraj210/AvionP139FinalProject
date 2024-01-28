using AutoMapper;
using Avion.Areas.Admin.ViewModels.Tag;
using Avion.Areas.Admin.ViewModels.Testimonial;
using Avion.Data;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class TagService:ITagService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TagService(AppDbContext context,
                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TagVM>> GetAllAsync()
        {
            return _mapper.Map<List<TagVM>>(await _context.Tags.ToListAsync());
        }
        public async Task<List<TagVM>> GetAllIgnoreAdminAsync()
        {
            return _mapper.Map<List<TagVM>>(await _context.Tags.IgnoreQueryFilters().ToListAsync());
        }

        public async Task<TagVM> GetByIdIgnoreAsync(int id)
        {
            var datas = await _context.Tags.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);
            TagVM tag = _mapper.Map<TagVM>(datas);
            return tag;
        }

        public async Task CreateAsync(TagCreateVM tag)
        {
            Tag dbTag = _mapper.Map<Tag>(tag);

            await _context.Tags.AddAsync(dbTag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Tag dbTag = await _context.Tags.IgnoreQueryFilters().Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Tags.Remove(dbTag);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(TagVM request)
        {


            if (request.SoftDeleted)
            {
                request.SoftDeleted = false;
            }
            else
            {
                request.SoftDeleted = true;
            }

            Tag dbTag = await _context.Tags.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == request.Id);
            _mapper.Map(request, dbTag);
            _context.Tags.Update(dbTag);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(TagEditVM tag)
        {
            Tag dbTag = await _context.Tags.IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync(m => m.Id == tag.Id);

            dbTag.CreateTime = DateTime.Now;

            _mapper.Map(tag, dbTag);

            _context.Tags.Update(dbTag);

            await _context.SaveChangesAsync();
        }
        public async Task<TagVM> GetByIdWithoutTrackingAsync(int id)
        {
            return _mapper.Map<TagVM>(await _context.Tags.IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync(m => m.Id == id));
        }

        public async Task<TagVM> GetByNameWithoutTrackingAsync(string name)
        {
            return _mapper.Map<TagVM>(await _context.Tags.IgnoreQueryFilters().AsNoTracking()
                                                         .FirstOrDefaultAsync(m => m.Name.Trim().ToLower() == name.Trim().ToLower()));
        }

        public List<SelectListItem> GetAllSelectedAsync()
        {
            return _context.Tags.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.Id.ToString(),

            }).ToList();
        }
    }
}
