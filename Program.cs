using BlogApp.Data;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlogAppDbContext>(options=>{
    options.UseSqlite(builder.Configuration.GetConnectionString("sql_connection"));
});

var app = builder.Build();

SeedData.CreateTestData(app);

app.MapGet("/", () => "Hello World!");

app.Run();
