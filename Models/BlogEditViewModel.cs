using System.ComponentModel.DataAnnotations;
using BlogApp.Entity;

namespace BlogApp.Models
{
    
    public class BlogEditViewModel
    {
        [Display(Name = "Blog Title")]
        public string? BlogTitle { get; set; }

        [Display(Name = "Blog Content")]
        public string? BlogContent { get; set; }

        [Display(Name = "Blog Description")]
        public string? BlogDescription { get; set; }

        [Display(Name = "Blog Image")]
        public string? BlogImage { get; set; }

        public List<int> SelectedTags { get; set; } = new List<int>();

        public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
    }
}