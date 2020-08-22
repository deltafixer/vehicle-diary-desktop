using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static VehicleDiary.Models.Enums.UserEnums;

namespace VehicleDiary.Models
{
    [Table("PersonUser")]
    public class PersonUserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(20)]
        
        public string LastName { get; set; }
        
        public PersonType PersonType { get; set; }
        
        [Required]
        public UserModel User { get; set; }
    }
}
