
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "EmailRequired")]
        [EmailAddress(ErrorMessage = "InvalidEmail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PasswordRequired")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "RememberMeLabel")]
        public bool RememberMe { get; set; }

    }
}
