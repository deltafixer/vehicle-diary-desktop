using System;
using System.Windows;
using VehicleDiary.Models;
using VehicleDiary.Services;
using static VehicleDiary.Models.Enums.VehicleEnums;

namespace VehicleDiary.Main.ViewModels
{
    public class VehicleViewModel
    {
        private readonly VehicleModel _vehicle;
        private readonly UniversalCRUDService<VehicleModel> _vehicleCrudService;
        private readonly UniversalCRUDService<VehicleSpecificationModel> _vehicleSpecificationCrudService;
        
        public VehicleViewModel(VehicleModel vehicle, UniversalCRUDService<VehicleModel> vehicleCrudService, UniversalCRUDService<VehicleSpecificationModel> vehicleSpecificationCrudService)
        {
            _vehicle = vehicle;
            _vehicleCrudService = vehicleCrudService;
            _vehicleSpecificationCrudService = vehicleSpecificationCrudService;
        }

        public string Vin => _vehicle.Vin;
        public Make Make => _vehicle.Make;
        public Model Model => _vehicle.Model;

        public event EventHandler VehicleRemoved;

        public async void Remove()
        {
            try
            {
                await _vehicleSpecificationCrudService.Delete(_vehicle.VehicleSpecification.Id);
                await _vehicleCrudService.Delete(_vehicle.Vin);
                VehicleRemoved?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured while deleting this vehicle", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
