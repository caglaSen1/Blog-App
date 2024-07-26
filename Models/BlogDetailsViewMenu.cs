using BlogApp.Entity;

namespace BlogApp.Models
{

    public class BlogDetailsViewModel{

        public Blog Blog = null!;
        public int CommentCount;
        public int LikeCount;
    }
}