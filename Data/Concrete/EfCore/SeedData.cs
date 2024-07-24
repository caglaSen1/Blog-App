using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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
                        new Tag("web prograglama", TagColors.primary),
                        new Tag("full-stack", TagColors.secondary),
                        new Tag("game", TagColors.success),
                        new Tag("backend", TagColors.danger),
                        new Tag("frontend", TagColors.info)
                    );
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User("ahmetkaya", "Ahmet", "Kaya" , "kaya@gmail.com", "123456", null),
                        new User("sinankarabulut", "Sinan", "Karabulut", "kaya@gmail.com", "123456", null)
                    );
                    context.SaveChanges();
                }

                if (!context.Blogs.Any())
                {
                    var Tags = context.Tags.ToList();

                    var Blog1 = new Blog("Asp.net Core", "asp.net core güzel bir kütüphanedir.", "Asp.net core güzel bir kütüphanedir.", "1.png", 1);

                    Blog1.Comments.Add(new Comment("Gayet iyi bir anlatım olmuş.", Blog1.Id, 1));
                    Blog1.Tags.AddRange(Tags.Take(3).ToList());

                    var Blog2 = new Blog("Unity ile oyun geliştirme", "Unity editörü ile oyunlar geliştirebilirsiniz.", "Unity editörü ile oyunlar geliştirebilirsiniz.", "2.png", 2);

                    Blog2.Comments.Add(new Comment("Harika bir oyun olmuş.", Blog2.Id, 2));        
                    Blog2.Tags.AddRange(Tags.Take(2).ToList());

                    var Blog3 = new Blog("Full Stack Developer Olmak", "Full Stack Developer Olmak Güzeldir.", "Full Stack Developer Olmak Güzeldir.", "3.png", 1);

                    Blog3.Comments.Add(new Comment("Full Stack Developer olmak için neler yapmalıyım?", Blog3.Id, 1));
                    Blog3.Tags.AddRange(Tags.Take(4).ToList());

                    context.Blogs.AddRange(Blog1, Blog2, Blog3);

                    context.SaveChanges();
                }
            }
        }
    }
}