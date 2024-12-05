
using System.Numerics;

namespace iLib.Models
{
 
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserRole { get; set; }
        public string? UserEmail { get; set; }
        public long UserPhoneNumber { get; set; }
    }
}

