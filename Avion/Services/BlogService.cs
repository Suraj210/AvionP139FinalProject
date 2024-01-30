using AutoMapper;
using Avion.Areas.Admin.ViewModels.Blog;
using Avion.Data;
using Avion.Helpers.Extentions;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;


        public BlogService(AppDbContext context,
                             IMapper mapper,
                             IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }


        //Get Blogs with its Category by Take function
        public async Task<List<BlogVM>> GetAllByTakeWithCategoryAsync(int take)
        {
            List<Blog> blogs = await _context.Blogs.OrderByDescending(m => m.CreateTime)
                                                 .Take(take)
                                                 .Include(m => m.BlogCategory)
                                                 .ToListAsync();

            return _mapper.Map<List<BlogVM>>(blogs);
        }


        //Get List Blogs with its all datas in paginated format
        public async Task<List<BlogVM>> GetPaginatedDatasAsync(int page, int take)
        {
            List<Blog> blogs = await _context.Blogs.OrderByDescending(m => m.CreateTime)
                                                   .Include(bc => bc.BlogCategory)
                                                   .Include(m => m.BlogTags)
                                                   .ThenInclude(m => m.Tag)
                                                   .Skip((page * take) - take)
                                                   .Take(take)
                                                   .ToListAsync();
            return _mapper.Map<List<BlogVM>>(blogs);
        }

        //Get Blog Count
        public async Task<int> GetCountAsync()
        {
            return await _context.Blogs.CountAsync();
        }


        //Get Blog Count by Category
        public async Task<int> GetCountByCategoryAsync(int id)
        {
            return await _context.Blogs.Where(m => m.BlogCategoryId == id).CountAsync();
        }

        //Get particular Blog by its Id
        public async Task<BlogVM> GetByIdAsync(int id)
        {
            Blog blog = await _context.Blogs.Where(m => m.Id == id)
                                            .Include(bc => bc.BlogCategory)
                                            .Include(m => m.BlogTags)
                                            .ThenInclude(m => m.Tag)
                                            .FirstOrDefaultAsync();

            return _mapper.Map<BlogVM>(blog);

        }



        //Get list of Blogs by categoryname

        public async Task<List<BlogVM>> GetPaginatedDatasByCategoryAsync(int id, int page, int take)
        {
            List<Blog> blogs = await _context.Blogs.Where(m => m.BlogCategoryId == id)
                                                   .OrderByDescending(m => m.CreateTime)
                                                   .Include(bc => bc.BlogCategory)
                                                   .Include(m => m.BlogTags)
                                                   .ThenInclude(m => m.Tag)
                                                   .Skip((page * take) - take)
                                                   .Take(take)
                                                   .ToListAsync();
            return _mapper.Map<List<BlogVM>>(blogs);
        }



        //For Admin Panel


        public async Task<BlogVM> GetByNameWithoutTrackingAsync(string name)
        {
            Blog blog = await _context.Blogs.Where(m => m.Title.Trim().ToLower() == name.Trim().ToLower()).FirstOrDefaultAsync();

            return _mapper.Map<BlogVM>(blog);
        }

        //Get List Blogs with its all datas in paginated format with Ignore Queryy Filters
        public async Task<List<BlogVM>> GetPaginatedDatasWithIgnoreQuerryAsync(int page, int take)
        {
            List<Blog> blogs = await _context.Blogs.IgnoreQueryFilters()
                                                   .OrderByDescending(m => !m.SoftDeleted)
                                                   .Include(bc => bc.BlogCategory)
                                                   .Include(m => m.BlogTags)
                                                   .ThenInclude(m => m.Tag)
                                                   .Skip((page * take) - take)
                                                   .Take(take)
                                                   .ToListAsync();
            return _mapper.Map<List<BlogVM>>(blogs);
        }

        //Get Blog Count with Ignore Querry Filters
        public async Task<int> GetCountWithIgnoreFilterAsync()
        {
            return await _context.Blogs.IgnoreQueryFilters().CountAsync();
        }

        //Get particular Blog by its Id with Ignore QueryFilters
        public async Task<BlogVM> GetByIdIgnoreAsync(int id)
        {
            Blog blog = await _context.Blogs.IgnoreQueryFilters()
                                            .Where(m => m.Id == id)
                                            .Include(bc => bc.BlogCategory)
                                            .Include(m => m.BlogTags)
                                            .ThenInclude(m => m.Tag)
                                            .FirstOrDefaultAsync();

            if (blog.BlogCategory.SoftDeleted)
            {
                blog.BlogCategory = null;
            }

            return _mapper.Map<BlogVM>(blog);

        }

        //Soft delete Blog 
        public async Task SoftDeleteAsync(BlogVM request)
        {


            if (request.SoftDeleted)
            {
                request.SoftDeleted = false;
            }
            else
            {
                request.SoftDeleted = true;
            }

            Blog dbBlog = await _context.Blogs.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == request.Id);
            _mapper.Map(request, dbBlog);
            _context.Blogs.Update(dbBlog);
            await _context.SaveChangesAsync();
        }

        //Create Blog Post
        public async Task CreateAsync(BlogCreateVM blog)
        {

            string fileName = $"{Guid.NewGuid()}-{blog.Photo.FileName}";

            string path = _env.GetFilePath("assets/images", fileName);


            var selectedTags = blog.Tags.Where(m => m.Selected).Select(m => m.Value).ToList();

            var dbBlog = new Blog()
            {
                Title = blog.Title,
                Description = blog.Description,
                BlogCategoryId = blog.BlogCategoryId,
                Image = fileName,
            };

            foreach (var item in selectedTags)
            {
                dbBlog.BlogTags.Add(new BlogTag()
                {
                    TagId = int.Parse(item)
                });
            }

            await _context.Blogs.AddAsync(dbBlog);
            await _context.SaveChangesAsync();
            await blog.Photo.SaveFileAsync(path);

        }

        //Delete Blog Post
        public async Task DeleteAsync(int id)
        {
            Blog dbBlog = await _context.Blogs.IgnoreQueryFilters()
                                              .Where(m=>m.Id==id)
                                              .Include(bc => bc.BlogCategory)
                                              .Include(m => m.BlogTags)
                                              .ThenInclude(m => m.Tag)
                                              .FirstOrDefaultAsync();


            _context.Blogs.Remove(dbBlog);
            await _context.SaveChangesAsync();




            string path = _env.GetFilePath("assets/images", dbBlog.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }



        }

        //Edit Blog Post
        public async Task EditAsync(BlogEditVM blog)
        {
           

            if (blog.Photo != null)
            {
                string fileName = $"{Guid.NewGuid()}-{blog.Photo.FileName}";

                string path = _env.GetFilePath("assets/images", fileName);
                blog.Image = fileName;
                await blog.Photo.SaveFileAsync(path);
            }

            


            Blog blogById = await _context.Blogs.IgnoreQueryFilters().Include(m => m.BlogTags).FirstOrDefaultAsync(m => m.Id == blog.Id);

            var existingIds = blogById.BlogTags.Select(m => m.TagId).ToList();

            var selectedIds = blog.Tags.Where(m => m.Selected).Select(m => m.Value).Select(int.Parse).ToList();

            var toAdd = selectedIds.Except(existingIds);
            var toRemove = existingIds.Except(selectedIds);

            blogById.BlogTags = blogById.BlogTags.Where(m => !toRemove.Contains(m.TagId)).ToList();

            foreach (var item in toAdd)
            {
                blogById.BlogTags.Add(new BlogTag
                {
                    TagId = item
                });
            }



            _mapper.Map(blog, blogById);

            _context.Blogs.Update(blogById);

            await _context.SaveChangesAsync();
        }



    }
}
