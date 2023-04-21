using System;
using System.Collections.Generic;

namespace WebBDS.Models
{
    public partial class User
    {
        public User()
        {
            UserRequests = new HashSet<UserRequest>();
        }

        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime Dob { get; set; }
        public bool Gender { get; set; }
        public int RoleId { get; set; }
        public string? ImgAvar { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<UserRequest> UserRequests { get; set; }
    }
}
