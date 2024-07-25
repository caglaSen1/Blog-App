using BlogApp.Data;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogAppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sql_connection"));
});


builder.Services.AddScoped<IBlogRepository, EfBlogRepository>();
builder.Services.AddScoped<ITagRepository, EfTagRepository>();
builder.Services.AddScoped<ICommentRepository, EfCommentRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

SeedData.CreateTestData(app);

app.MapControllerRoute(
    name: "/admin/home",
    pattern: "/admin/home",
    defaults: new { controller = "Admin", action = "Home" });

    app.MapControllerRoute(
    name: "/admin/edit",
    pattern: "/admin/edit/{url}",
    defaults: new { controller = "Admin", action = "Edit" });

    app.MapControllerRoute(
    name: "/admin/delete",
    pattern: "/admin/delete/{url}",
    defaults: new { controller = "Admin", action = "Delete" });

app.MapControllerRoute(
    name: "user/register",
    pattern: "user/register",
    defaults: new { controller = "User", action = "Register" });

app.MapControllerRoute(
    name: "user/login",
    pattern: "user/login",
    defaults: new { controller = "User", action = "Login" });

app.MapControllerRoute(
name: "user/logout",
pattern: "user/logout",
defaults: new { controller = "User", action = "Logout" });

app.MapControllerRoute(
    name: "blog_create",
    pattern: "blog/create",
    defaults: new { controller = "Blog", action = "Create" });

app.MapControllerRoute(
    name: "blog_details",
    pattern: "blog/details/{url}",
    defaults: new { controller = "Blog", action = "Details" });

app.MapControllerRoute(
name: "blog_by_tag",
pattern: "blog/tag/{tagUrl}",
defaults: new { controller = "Blog", action = "List" });

app.MapControllerRoute(
name: "blog_list_by_user",
pattern: "blog/listByUser",
defaults: new { controller = "Blog", action = "ListByUser" });

app.MapControllerRoute(
name: "blog_delete",
pattern: "blog/delete/{url}",
defaults: new { controller = "Blog", action = "Delete" });

app.MapControllerRoute(
name: "blog_edit",
pattern: "blog/edit/{url}",
defaults: new { controller = "Blog", action = "Edit" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=List}/{id?}");

app.Run();
