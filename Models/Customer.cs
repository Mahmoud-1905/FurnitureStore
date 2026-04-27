namespace FurnitureStore.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Note: In a real app, we would hash this, but for this test I'll keep it simple as requested.
        public string FullName { get; set; }
    }
}
