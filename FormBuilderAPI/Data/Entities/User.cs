using System.ComponentModel.DataAnnotations;

namespace FormBuilderAPI.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; } // Hash passwords in production
    }
}