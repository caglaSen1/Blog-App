using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data{

    public class BlogAppDbContext : DbContext{
        public BlogAppDbContext(DbContextOptions<BlogAppDbContext> options): base(options){}

        public DbSet<Blog> Blogs => Set<Blog>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<User> Users => Set<User>();
    }
}