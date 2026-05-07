using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        [Required]
        [MaxLength(450)]
        public string UserId { get; set; } = string.Empty;

        public DateTime AppointmentDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        [MaxLength(1000)]
        public string? ProductInterests { get; set; }

        [MaxLength(500)]
        public string? ManagerNotes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ApplicationUser User { get; set; } = null!;
    }
}
