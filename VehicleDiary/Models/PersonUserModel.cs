using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleDiary.Models
{
    [Table("PersonUser")]
    public class PersonUserModel : UserModel
    {
        public PersonUserModel()
        {
            Vehicles = new HashSet<VehicleModel>();
        }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        
        public ICollection<VehicleModel> Vehicles { get; set; }
    }
}
