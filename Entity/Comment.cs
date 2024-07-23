using System.ComponentModel.DataAnnotations;

namespace BlogApp.Entity
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
        /*
        public bool IsActive { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } */
    }
}