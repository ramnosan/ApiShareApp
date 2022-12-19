using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AspServer.Models
{
    [PrimaryKey(nameof(Id))]
    public class User
    {
        [ID]
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

        public User(RegisterInput regInp)
        {
            this.Name= regInp.Name;
            this.Email= regInp.Email;
            this.Password= regInp.Password;
            this.Id = null;
        }
        public User() { }
    }
}
