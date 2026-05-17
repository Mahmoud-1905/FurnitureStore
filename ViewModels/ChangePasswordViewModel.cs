using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "EmailRequired")]
        [EmailAddress(ErrorMessage = "InvalidEmail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PasswordRequired")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "PasswordLengthError")]
        [DataType(DataType.Password)]
        [Display(Name = "NewPasswordLabel")]
        [Compare("ConfirmNewPassword", ErrorMessage = "PasswordMismatch")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "ConfirmPasswordRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmNewPasswordLabel")]
        public string ConfirmNewPassword { get; set; }

        public string Token { get; set; }
    }
}