using System.ComponentModel.DataAnnotations;

namespace BlogApp.Entity
{
    public class Comment
    {
        [Key]
        [Display(Name = "Comment Id")]
        public int Id { get; set; }

        [Display(Name = "Comment Text")]
        [Required(ErrorMessage = "Text is required.")]
        public string? Text { get; set; } 

        [Display(Name = "Comment Created At")]
        public DateTime CreatedAt { get; set;}

        [Display(Name = "Comment Updated At")]
        public DateTime UpdatedAt { get; set; }

        [Display(Name = "Blog Of Comment")]
        public int BlogId { get; set; }
        public Blog Blog { get; set; } = null!;

        [Display(Name = "Author Of Comment")]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public Comment(string text, int blogId, int userId)
        {
            Text = text;
            CreatedAt = DateTime.Now;
            BlogId = blogId;
            UserId = userId;
        }
        
    }
}