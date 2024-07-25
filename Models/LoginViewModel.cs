using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models{

    public class LoginViewModel{

        
        [EmailAddress]
        [Display(Name = "Eposta")]
        [Required(ErrorMessage = "Lütfen eposta adresinizi giriniz.")]
        public string? Email {get;set;}
        
        [StringLength(20, ErrorMessage = "{0} alanı en az {2} an çok {1} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        [Required(ErrorMessage = "Lütfen parolanızı giriniz.")]
        public string? Password {get;set;}
    }
}