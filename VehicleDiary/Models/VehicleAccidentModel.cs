using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleDiary.Models
{
    [Table("VehicleAccident")]
    class VehicleAccidentModel
    {
        public int Id { get; set; }
        [Required]
        public VehicleModel Vehicle { get; set; }
        public DateTime DateOfAccident { get; set; }
        public float DamagePriceEvaluation { get; set; }
    }
}
