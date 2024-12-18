using System.Text.Json.Serialization;

namespace thoeun_coffee.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // 'customer', 'staff', 'admin'
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // [JsonIgnore]
        public List<Order> orders{ get; set; } = new List<Order>();
        // [JsonIgnore]
        public List<Review> Reviews { get; set; } = new List<Review>();
        // [JsonIgnore]
        public List<StaffShift> StaffShifts { get; set; } = new List<StaffShift>();
    }
}
