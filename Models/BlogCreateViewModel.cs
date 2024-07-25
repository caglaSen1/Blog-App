using System.ComponentModel.DataAnnotations;
using BlogApp.Entity;

namespace BlogApp.Models
{
    
    public class BlogCreateViewModel
    {
        [Display(Name = "Blog Title")]
        [Required(ErrorMessage = "Title is required.")]
        public string BlogTitle { get; set; }

        [Display(Name = "Blog Content")]
        [Required(ErrorMessage = "Content is required.")]
        public string BlogContent { get; set; }

        [Display(Name = "Blog Description")]
        public string? BlogDescription { get; set; }

        [Display(Name = "Blog Image")]
        public string? BlogImage { get; set; }

        public List<int> SelectedTags { get; set; } = new List<int>();

        public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
    }
}