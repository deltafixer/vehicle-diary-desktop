using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleDiary.Models
{
    [Table("PersonUserVehicle")]
    public class PersonUserVehicleModel
    {
        [Key, Column(Order = 1)]
        [MaxLength(30)]
        public string Username { get; set; }
        [Key, Column(Order = 2)]
        public string Vin { get; set; }
        public PersonUserModel PersonUser { get; set; }
        public VehicleModel Vehicle { get; set; }
    }
}