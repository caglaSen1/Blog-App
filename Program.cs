using BlogApp.Data;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogAppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sql_connection"));
});


builder.Services.AddScoped<IBlogRepository, EfBlogRepository>();
builder.Services.AddScoped<ITagRepository, EfTagRepository>();

var app = builder.Build();

app.UseStaticFiles();

SeedData.CreateTestData(app);

app.MapControllerRoute(
    name: "blog_details",
    pattern: "blog/{url}",
    defaults: new { controller = "Blog", action = "Details" });

app.MapControllerRoute(
name: "blog_by_tag",
pattern: "blog/tag/{tagUrl}",
defaults: new { controller = "Blog", action = "List" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Index}/{id?}");

app.Run();
