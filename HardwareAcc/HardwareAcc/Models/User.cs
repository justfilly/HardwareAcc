using System.ComponentModel.DataAnnotations;

namespace HardwareAcc.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; } = null!;

        [Required]
        [StringLength(60)]
        public string Password { get; set; } = null!;

        [Required]
        public int RoleId { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }
        
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string SecondName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Patronymic { get; set; } = null!;
    }
}
