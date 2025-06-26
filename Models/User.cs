using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VarelaCarWash.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; } = false;

        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}