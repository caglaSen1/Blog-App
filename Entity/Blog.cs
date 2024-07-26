using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Web;

namespace BlogApp.Entity
{
    public class Blog
    {
        [Key]
        [Display(Name = "Blog Id")]
        public int Id { get; set; }

        [Display(Name = "Blog Title")]
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Display(Name = "Blog Url")]
        public string Url { get; set; }

        [Display(Name = "Blog Content")]
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        [Display(Name = "Blog Description")]
        public string? Description { get; set; }

        [Display(Name = "Blog Image")]
        public string? Image { get; set; }

        [Display(Name = "Blog Created At")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Blog Updated At")]
        public DateTime UpdatedAt { get; set; }

        [Display(Name = "Blog Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Blog Like Count")]
        public int LikeCount { get; set; } = 0;

        [Display(Name = "Blog Author")]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [Display(Name = "Blog Tags")]
        public List<Tag> Tags { get; set; } = new List<Tag>();

        [Display(Name = "Blog Comments")]
        public List<Comment> Comments { get; set; } = new List<Comment>();

        public Blog(string title, string content, string? description, string? image, int userId)
        {
            Title = title;
            Url = Title.ToLower().Replace(" ", "-");
            Content = content;
            Description = description;
            if (image == null)
            {
                Image = "defaultBlog.png";
            }
            else
            {
                Image = image;
            }
            Image = image;
            CreatedAt = DateTime.Now;
            IsActive = true;
            UserId = userId;
        }
    }
}