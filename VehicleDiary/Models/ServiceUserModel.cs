using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static VehicleDiary.Models.Enums.UserEnums;

namespace VehicleDiary.Models
{
    [Table("ServiceUser")]
    class ServiceUserModel : UserModel
    {
		[Required]
		public string Name { get; set; }
		public ServiceType ServiceType { get; set; }
		public ICollection<VehicleServiceModel> Services { get; set; }

	}
}
