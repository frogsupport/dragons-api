using System;

namespace Dragons.Api.Models
{
    public class User
    {
        // The user's username
        // Users are unique by username
        public string Username { get; set; }

        // The user's name
        public string FirstName { get; set; }

        // The user's last name
        public string LastName { get; set; }

        // The uer's password
        public string Password { get; set; }
    }
}
