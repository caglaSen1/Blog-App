using BlogApp.Entity;

namespace BlogApp.Models
{
    public class BlogCreateViewModel
    {
        public Blog Blog { get; set; } = new Blog();
        public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
    }
}