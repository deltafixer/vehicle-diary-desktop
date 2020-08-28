using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static VehicleDiary.Models.Enums.VehicleEnums;

namespace VehicleDiary.Models
{
    [Table("VehicleSpecification")]
    public class VehicleSpecificationModel
    {
        public int Id { get; set; }
        public DateTime MakeDate { get; set; }
        public BodyStyle BodyStyle { get; set; }
        public DriveType DriveType { get; set; }
        public float Kilometrage { get; set; }
        public FuelType FuelType { get; set; }
        public int EngineVolume { get; set; }
        public int EnginePowerKW { get; set; }
        public EngineEmissionType EngineEmissionType { get; set; }
        public GearboxType GearboxType { get; set; }
        [Required]
        public VehicleModel Vehicle { get; set; }
    }
}
