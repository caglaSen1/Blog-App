using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models{

    public class RegisterViewModel{

        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Lütfen adınızı giriniz.")]
        public string FirstName {get;set;}

        [Display(Name = "Soyad")]
        [Required(ErrorMessage = "Lütfen soyadınızı giriniz.")]
        public string LastName {get;set;}

        
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen kullanıcı adınızı giriniz.")]
        public string UserName {get;set;}

        
        [EmailAddress]
        [Display(Name = "Eposta")]
        [Required(ErrorMessage = "Lütfen eposta adresinizi giriniz.")]
        public string Email {get;set;}

        [StringLength(20, ErrorMessage = "{0} alanı en az {2} an çok {1} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        [Required(ErrorMessage = "Lütfen parola oluşturunuz.")]
        public string Password {get;set;}

        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        [Compare(nameof(Password), ErrorMessage = "Palolalarınız eşleşmedi.")]
        [Display(Name = "Parola Tekrar")]
        [Required(ErrorMessage = "Lütfen parolanızı tekrar giriniz.")]
        public string ConfirmPassword {get;set;}  
    }
}