using BlogApp.Entity;

namespace BlogApp.Models
{

    public class BlogViewModel{

        public List<Blog> Blogs {get;set;} = new();
        public List<Tag> Tags {get;set;} = new();
    }
}