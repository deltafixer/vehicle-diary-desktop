using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static VehicleDiary.Models.Enums.UserEnums;

namespace VehicleDiary.Models
{
    // COMMENT: 'User' is apparantly a keyword :D
    [Table("UserTable")]
    public class UserModel
    {
        [Key]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        // TODO: user doesn't choose his role
        public Role Role { get; set; }

        public PersonUserModel PersonUser { get; set; }

        public ServiceUserModel ServiceUser { get; set; }
    }
}