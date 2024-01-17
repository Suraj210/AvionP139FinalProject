using Avion.Data;
using Avion.Models;
using Avion.Services;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


builder.Services.AddScoped<IHeroService, HeroService>();
builder.Services.AddScoped<IAdvertService, AdvertService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<IIdeaService, IdeaService>();
builder.Services.AddScoped<ITestimonialService, TestimonialService>();
builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddScoped<ISliderService, SliderServive>();
//builder.Services.AddScoped<IReviewService, ReviewService>();
//builder.Services.AddScoped<IBlogService, BlogService>();
//builder.Services.AddScoped<ITagService, TagService>();
//builder.Services.AddScoped<ISettingService, SettingService>();
//builder.Services.AddScoped<ILayoutService, LayoutService>();
//builder.Services.AddScoped<IAboutContentService, AboutContentService>();
//builder.Services.AddScoped<IBrandService, BrandService>();
//builder.Services.AddScoped<ITeamService, TeamService>();
//builder.Services.AddScoped<IContactService, ContactService>();
//builder.Services.AddScoped<ISubscribeService, SubscribeService>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var supportedCultures = new[] { new CultureInfo("en-US") }; // Adjust as needed
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();


//app.UseSession();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();