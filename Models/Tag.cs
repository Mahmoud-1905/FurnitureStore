using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class Tag
    {
        public int TagId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        // Navigation property
        public ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
    }
}
