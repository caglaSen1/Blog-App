using System.ComponentModel.DataAnnotations;
using BlogApp.Entity;

namespace BlogApp.Models
{

    public class TagCreateViewModel
    {
        [Display(Name = "Tag Name")]
        [Required(ErrorMessage = "Lütfen bir etiket adı giriniz.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Tag Colors")]
        public List<string> Colors { get; set; } = new List<string>();

        [Display(Name = "Selected Tag Color")]
        public string SelectedColorStr { get; set; } = TagColors.cornflowerblue.ToString(); 
    }

}