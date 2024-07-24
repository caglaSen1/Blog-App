using System.ComponentModel.DataAnnotations;

namespace BlogApp.Entity
{
    public class User
    {
        [Key]
        [Display(Name = "User Id")]
        public int Id { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User name is required.")]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Display(Name = "User Email")]
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "User Password")]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "User Image")]
        public string? Image { get; set; }

        [Display(Name = "User Created At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "User Updated At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedAt { get; set; }

        [Display(Name = "User Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "User Blogs")]
        public List<Blog> Blogs { get; set; } = new List<Blog>();

        [Display(Name = "User Comments")]
        public List<Comment> Comments { get; set; } = new List<Comment>();

        public User(string userName, string firstName, string lastName, string email, string password, string? image)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            if (image == null)
            {
                Image = "defaultUser.png";
            }
            else
            {
                Image = image;
            }
            CreatedAt = DateTime.Now;
            IsActive = false;
        }

    }
}