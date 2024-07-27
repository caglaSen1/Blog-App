using BlogApp.Entity;

namespace BlogApp.Models
{

    public class BlogViewModel
    {
        public List<Blog> Blogs { get; set; } = new();
        public PagedResult<Blog> PagedBlogs { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public string SelectedTagUrl { get; set; }
        public string SearchString { get; set; }
    }
}