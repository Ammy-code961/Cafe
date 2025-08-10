using System;
using System.ComponentModel.DataAnnotations;

namespace CafeApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string? Username { get; set; } = null!;

        [Required]
        [StringLength(255)] // Allow longer password hash
        public string? Password { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }
    }
}
