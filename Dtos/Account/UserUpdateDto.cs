using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coffee_api.Dtos
{
    public class UserUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
    }
}