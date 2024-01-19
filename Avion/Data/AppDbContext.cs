using Avion.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Avion.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Hero> Heros { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<BrandCategory> BrandCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hero>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Advert>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Feature>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Idea>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Testimonial>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Brand>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<ProductImage>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Tag>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<BlogCategory>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Subscribe>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Setting>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<About>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<ContactMessage>().HasQueryFilter(m => !m.SoftDeleted);
        }
    }
}
