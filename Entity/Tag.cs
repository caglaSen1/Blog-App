using System.ComponentModel.DataAnnotations;

namespace BlogApp.Entity
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Blog> Blogs { get; set; } = new List<Blog>();
    }
}