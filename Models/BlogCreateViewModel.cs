using System.ComponentModel.DataAnnotations;
using BlogApp.Entity;

namespace BlogApp.Models
{
    
    public class BlogCreateViewModel
    {
        [Required(ErrorMessage = "Title is required.")]
        public string BlogTitle { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string BlogContent { get; set; }
        public string? BlogDescription { get; set; }
        public string? BlogImage { get; set; }
        public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
    }
}