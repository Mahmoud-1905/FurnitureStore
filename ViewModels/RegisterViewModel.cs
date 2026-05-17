
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "NameRequired")]
        public string Name { get; set; }

        [Required(ErrorMessage = "EmailRequired")]
        [EmailAddress(ErrorMessage = "InvalidEmail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PasswordRequired")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "PasswordLengthError")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "PasswordMismatch")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPasswordRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPasswordLabel")]
        public string ConfirmPassword { get; set; }

    }
}
