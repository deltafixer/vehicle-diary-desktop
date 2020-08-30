using System;
using VehicleDiary.Models;

namespace VehicleDiary.Main.ViewModels
{
    public class VehicleAccidentViewModel
    {
        private readonly VehicleAccidentModel _vehicleAccident;

        public VehicleAccidentViewModel(VehicleAccidentModel vehicleAccidentModel)
        {
            _vehicleAccident = vehicleAccidentModel;
        }

        public DateTime DateOfAccident => _vehicleAccident.DateOfAccident;
        public float DamagePriceEvaluation => _vehicleAccident.DamagePriceEvaluation;
    }
}
