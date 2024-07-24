using System.ComponentModel.DataAnnotations;

namespace BlogApp.Entity
{
    public class Tag
    {
        [Key]
        [Display(Name = "Tag Id")]
        public int Id { get; set; }

        [Display(Name = "Tag Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Blog Url")]
        public string Url { get; set; }

        [Display(Name = "Tag Color")]
        public TagColors? Color { get; set; }

        [Display(Name = "Tag Of Blogs")]
        public List<Blog> Blogs { get; set; } = new List<Blog>();

        public Tag(string name, TagColors? color)
        {
            Name = name;
            Url = Name.ToLower().Replace(" ", "-");
            Color = color;
        }
    }

    public enum TagColors
    {
        primary, secondary, success, danger, warning, info, light, dark
    }

}