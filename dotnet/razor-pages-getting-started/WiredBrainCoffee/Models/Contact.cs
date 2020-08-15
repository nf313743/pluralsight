using System.ComponentModel.DataAnnotations;

namespace WiredBrainCoffee.Models
{
    public class Contact
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(10)]
        public string Message { get; set; }
    }
}