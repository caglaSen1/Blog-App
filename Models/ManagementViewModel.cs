using BlogApp.Entity;

namespace BlogApp.Models
{

    public class ManagementViewModel{

        public List<Blog> Blogs {get;set;} = new();

        public List<User> Users {get;set;} = new();
        public List<Tag> Tags {get;set;} = new();

        public List<Comment> Comments {get;set;} = new();
    }
}