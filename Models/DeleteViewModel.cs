using System.ComponentModel.DataAnnotations;
using BlogApp.Entity;

namespace BlogApp.Models
{

    public class DeleteViewModel
    {
        [Display(Name = "Entity Id")]
        public int EntityId { get; set; }

        [Display(Name = "Entity Name")]
        public string EntityName { get; set; }

        [Display(Name = "Action Url")]
        public string ActionUrl { get; set; } 

    }

}