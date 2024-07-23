using System.ComponentModel.DataAnnotations;

namespace BlogApp.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? UserName { get; set; }

        public List<Blog> Blogs { get; set; } = new List<Blog>();
        public List<Comment> Comments { get; set; } = new List<Comment>();

        /*
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }*/
    }
}