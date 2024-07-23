using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{

    public static class SeedData
    {

        public static void CreateTestData(IApplicationBuilder app)
        {

            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogAppDbContext>();

            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Tag { Name = "web prograglama" },
                        new Tag { Name = "full-stack" },
                        new Tag { Name = "game" },
                        new Tag { Name = "backend" },
                        new Tag { Name = "frontend" }
                    );
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { UserName = "ahmetkaya" },
                        new User { UserName = "sinankarabulut" }
                    );
                    context.SaveChanges();
                }

                if (!context.Blogs.Any())
                {
                    context.Blogs.AddRange(
                        new Blog
                        {
                            Title = "Asp.net Core",
                            Content = "asp.net core güzel bir kütüphanedir.",
                            Image = "1.jpg",
                            CreatedAt = DateTime.Now.AddDays(-15),
                            IsActive = true,
                            Tags = context.Tags.Take(3).ToList(),
                            UserId = 1
                        },
                        new Blog
                        {
                            Title = "Unity ile oyun geliştirme",
                            Content = "Unity editörü ile oyunlar geliştirebilirsiniz.",
                            Image = "2.jpg",
                            CreatedAt = DateTime.Now.AddDays(-10),
                            IsActive = true,
                            Tags = context.Tags.Take(2).ToList(),
                            UserId = 2
                        },
                        new Blog
                        {
                            Title = "Full Stack Developer Olmak",
                            Content = "Full Stack Developer Olmak Güzeldir.",
                            Image = "3.jpg",
                            CreatedAt = DateTime.Now.AddDays(-5),
                            IsActive = true,
                            Tags = context.Tags.Take(4).ToList(),
                            UserId = 1
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}