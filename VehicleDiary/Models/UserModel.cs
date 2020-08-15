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
        public string Password { get; set; }
        // TODO: user doesn't choose his role
        public Role Role { get; set; }
    }
}