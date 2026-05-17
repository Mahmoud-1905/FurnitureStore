using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    /// <summary>
    /// Append-only — no update or delete
    /// </summary>
    public class AuditLog
    {
        public int LogId { get; set; }

        [Required]
        [MaxLength(450)]
        public string ActorUserId { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string ActionType { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string TargetEntityType { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string TargetEntityId { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public string? OldValues { get; set; } // JSON snapshot before change

        public string? NewValues { get; set; } // JSON snapshot after change

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ApplicationUser ActorUser { get; set; } = null!;
    }
}
