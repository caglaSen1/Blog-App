using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models{

    public class RegisterViewModel{

        [Required]
        [Display(Name = "Ad")]
        public string FirstName {get;set;}

        [Required]
        [Display(Name = "Soyad")]
        public string LastName {get;set;}

        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName {get;set;}

        [Required]
        [EmailAddress]
        [Display(Name = "Eposta")]
        public string Email {get;set;}

        [Required]
        [StringLength(10, ErrorMessage = "{0} alanı en az {2} an çok {1} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password {get;set;}
    }
}