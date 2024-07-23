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
                        new Tag { Url = "web-programlama", Name = "web prograglama", Color = TagColors.primary },
                        new Tag { Url = "full-stack", Name = "full-stack", Color = TagColors.secondary },
                        new Tag { Url = "game", Name = "game", Color = TagColors.success },
                        new Tag { Url = "backend", Name = "backend", Color = TagColors.danger },
                        new Tag { Url = "frontend", Name = "frontend", Color = TagColors.info }
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
                            Url = "asp-net-core",
                            Title = "Asp.net Core",
                            Content = "asp.net core güzel bir kütüphanedir.",
                            Description = "Asp.net core güzel bir kütüphanedir.",
                            Image = "1.png",
                            CreatedAt = DateTime.Now.AddDays(-15),
                            IsActive = true,
                            Tags = context.Tags.Take(3).ToList(),
                            UserId = 1
                        },
                        new Blog
                        {
                            Url = "unity-ile-oyun-gelistirme",
                            Title = "Unity ile oyun geliştirme",
                            Content = "Unity editörü ile oyunlar geliştirebilirsiniz.",
                            Description = "Unity editörü ile oyunlar geliştirebilirsiniz.",
                            Image = "2.png",
                            CreatedAt = DateTime.Now.AddDays(-10),
                            IsActive = true,
                            Tags = context.Tags.Take(2).ToList(),
                            UserId = 2
                        },
                        new Blog
                        {
                            Url = "full-stack-developer-olmak",
                            Title = "Full Stack Developer Olmak",
                            Content = "Full Stack Developer Olmak Güzeldir.",
                            Description = "Full Stack Developer Olmak Güzeldir.",
                            Image = "3.png",
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