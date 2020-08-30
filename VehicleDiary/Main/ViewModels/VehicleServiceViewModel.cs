using System;
using VehicleDiary.Models;

namespace VehicleDiary.Main.ViewModels
{
    public class VehicleServiceViewModel
    {
        private readonly VehicleServiceModel _vehicleService;

        public VehicleServiceViewModel(VehicleServiceModel vehicleServiceModel)
        {
            _vehicleService = vehicleServiceModel;
        }
        public DateTime DateTaken => _vehicleService.DateTaken;
        public float Price => _vehicleService.Price;
        public string ServiceDetails => _vehicleService.ServiceDetails;
    }
}
