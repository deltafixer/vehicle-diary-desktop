using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static VehicleDiary.Models.Enums.UserEnums;

namespace VehicleDiary.Models
{
    [Table("PersonUser")]
    public class PersonUserModel
    {
        public PersonUserModel()
        {
            Vehicles = new HashSet<PersonUserVehicleModel>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(20)]
        
        public string LastName { get; set; }
        
        public UserType UserType { get; set; }
        
        public ICollection<PersonUserVehicleModel> Vehicles { get; set; }

        [Required]
        public UserModel User { get; set; }
    }
}
