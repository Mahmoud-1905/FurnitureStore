using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "EmailRequired")]
        [EmailAddress(ErrorMessage = "InvalidEmail")]
        public string Email { get; set; }

    }
}