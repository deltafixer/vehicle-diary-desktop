using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleDiary.Models
{
    [Table("VehicleService")]
    public class VehicleServiceModel
    {
        public int Id { get; set; }
        [Required]
        public VehicleModel Vehicle { get; set; }
        [Required]
        public ServiceUserModel ServicedBy { get; set; }
        public DateTime DateTaken { get; set; }
        public float Price { get; set; }
        public string ServiceDetails { get; set; }
    }
}
