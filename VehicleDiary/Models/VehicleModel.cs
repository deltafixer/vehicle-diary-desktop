﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static VehicleDiary.Models.Enums.VehicleEnums;

namespace VehicleDiary.Models
{
    [Table("Vehicle")]
    public class VehicleModel
    {
        [Key]
        public string Vin { get; set; }
        public Make Make { get; set; }
        public Model Model { get; set; }
        public VehicleSpecificationModel VehicleSpecification { get; set; }
        public ICollection<VehicleServiceModel> Services { get; set; }
        public ICollection<VehicleAccidentModel> Accidents { get; set; }
        public SaleListingModel SaleListing { get; set; }
    }
}
