using Caliburn.Micro;
using System;
using System.Windows;
using VehicleDiary.Main.Messages;
using VehicleDiary.Models;
using VehicleDiary.Services;
using static VehicleDiary.Models.Enums.VehicleEnums;

namespace VehicleDiary.Main.ViewModels
{
    public class VehicleViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly VehicleModel _vehicle;
        private readonly VehicleService _vehicleService;
        private readonly UniversalCRUDService<VehicleModel> _vehicleCrudService;
        private readonly UniversalCRUDService<VehicleSpecificationModel> _vehicleSpecificationCrudService;

        public VehicleViewModel(IEventAggregator eventAggregator, VehicleModel vehicle, VehicleService vehicleService, UniversalCRUDService<VehicleModel> vehicleCrudService, UniversalCRUDService<VehicleSpecificationModel> vehicleSpecificationCrudService)
        {
            _eventAggregator = eventAggregator;
            _vehicle = vehicle;
            _vehicleService = vehicleService;
            _vehicleCrudService = vehicleCrudService;
            _vehicleSpecificationCrudService = vehicleSpecificationCrudService;
        }

        public string Vin => _vehicle.Vin;
        public Make Make => _vehicle.Make;
        public Model Model => _vehicle.Model;

        public event EventHandler VehicleRemoved;

        public async void ViewReport()
        {
            try
            {
                VehicleModel vehicle = await _vehicleService.GetVehicleWithAllData(Vin);
                _vehicleService.Vehicle = vehicle;
                _eventAggregator.PublishOnUIThread(new MainNavigationMessage(MainNavigationMessages.VEHICLE_REPORT));
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured while acquiring the report for this vehicle", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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
