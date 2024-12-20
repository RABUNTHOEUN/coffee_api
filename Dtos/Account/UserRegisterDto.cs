using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coffee_api.Dtos.Account
{
    public class UserRegisterDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Role { get; set; } = "customer"; // Default to 'customer'
    }
}