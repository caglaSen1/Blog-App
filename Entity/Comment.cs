using System.ComponentModel.DataAnnotations;

namespace BlogApp.Entity
{
    public class Comment
    {
        [Key]
        [Display(Name = "Comment Id")]
        public int Id { get; set; }

        [Display(Name = "Comment Url")]
        public string Url { get; set; }

        [Display(Name = "Comment Text")]
        [Required(ErrorMessage = "Metin alanı boş bırakılamaz.")]
        public string? Text { get; set; } 

        [Display(Name = "Comment Created At")]
        public DateTime CreatedAt { get; set;}

        [Display(Name = "Comment Updated At")]
        public DateTime UpdatedAt { get; set; }

        [Display(Name = "Comment Is Active")]
        public bool IsActive { get; set; }

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
            Url = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff");
            IsActive = true;
            BlogId = blogId;
            UserId = userId;
        }
        
    }
}