using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using AspServer;

namespace AspServer
{
    [PrimaryKey(nameof(Id))]
    public class User
    {

        public int? Id { get; set; }
        [Required]
        [Key]
        public string? Name { get; set; }
        [Required]
        [Key]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string? Password { get; set; }
    }
}
