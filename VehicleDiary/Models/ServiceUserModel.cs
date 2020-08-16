using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static VehicleDiary.Models.Enums.UserEnums;

namespace VehicleDiary.Models
{
    [Table("ServiceUser")]
    public class ServiceUserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ServiceType ServiceType { get; set; }

        public ICollection<VehicleServiceModel> Services { get; set; }

        [Required]
        public UserModel User { get; set; }

    }
}
