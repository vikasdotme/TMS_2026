using System;
using System.ComponentModel.DataAnnotations;

namespace TMS_Models.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }  // Primary key, auto increment

        //[Required, MaxLength(50)]
        //public string Username { get; set; } // Username

        [Required, MaxLength(255)]
        public string PasswordHash { get; set; } // Hashed password

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } // Email (unique ideally)

        [Required, MaxLength(20)]
        public string? Role { get; set; } // Admin / Developer / User

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        //public DateTime? LastLogin { get; set; } // Optional
    }
}
