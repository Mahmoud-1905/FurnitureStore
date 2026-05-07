using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    /// <summary>
    /// Extends IdentityUser. Stored in AspNetUsers table.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(150)]
        public string FullName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public Cart? Cart { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    }
}
