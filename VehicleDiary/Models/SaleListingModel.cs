using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
