using System.ComponentModel.DataAnnotations;
using static VehicleDiary.Models.Enums.UserEnums;

namespace VehicleDiary.Models
{
    public abstract class UserModel
    {
        [Key]
        [MaxLength(30)]
        public string Username { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public Role Role { get; set; }
    }
}