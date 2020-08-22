using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleDiary.Models
{
    [Table("PersonUserVehicle")]
    public class PersonUserVehicleModel
    {
        [Key]
        public int Id { get; set; }
        
        [Key]
        public string Vin { get; set; }

        public PersonUserModel PersonUser { get; set; }
        
        public VehicleModel Vehicle { get; set; }
    }
}