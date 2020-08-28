using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static VehicleDiary.Models.Enums.VehicleEnums;

namespace VehicleDiary.Models
{
    [Table("SaleListing")]
    public class SaleListingModel
    {
        public int Id { get; set; }
        // not to have "unable to determine the principal end..."
        [Required]
        public VehicleModel Vehicle { get; set; }
        public float Price { get; set; }
        public DateTime DateAdded { get; set; }
        public Condition Condition { get; set; }
    }
}
