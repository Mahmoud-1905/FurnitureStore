using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class Address
    {
        public int AddressId { get; set; }

        [Required]
        [MaxLength(450)]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string AddressName { get; set; } = "Home"; // e.g., Home, Office

        [Required]
        [MaxLength(150)]
        public string StreetAddress { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string State { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Country { get; set; } = "Egypt";

        public bool IsDefault { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ApplicationUser User { get; set; } = null!;
    }
}
