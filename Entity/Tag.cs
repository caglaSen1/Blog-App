using System.ComponentModel.DataAnnotations;

namespace BlogApp.Entity
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string? Url { get; set; }
        public string? Name { get; set; }
        public TagColors? Color { get; set; }
        public List<Blog> Blogs { get; set; } = new List<Blog>();
    }

    public enum TagColors
    {
        primary, secondary, success, danger, warning, info, light, dark
    }
}