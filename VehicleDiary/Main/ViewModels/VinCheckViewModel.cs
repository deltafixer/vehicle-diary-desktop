using Caliburn.Micro;
using System;
using System.Windows;
using VehicleDiary.Main.Messages;
using VehicleDiary.Models;
using VehicleDiary.Services;

namespace VehicleDiary.Main.ViewModels
{
    public class VinCheckViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly VehicleService _vehicleService;
        private string _vin;

        public VinCheckViewModel(IEventAggregator eventAggregator, VehicleService vehicleService)
        {
            _eventAggregator = eventAggregator;
            _vehicleService = vehicleService;
        }

        public string Vin
        {
            get => _vin; set { _vin = value; NotifyOfPropertyChange(() => Vin); NotifyOfPropertyChange(() => CanGetVinReport); }
        }

        public bool CanGetVinReport => Vin?.Length == 17;

        public async void GetVinReport()
        {
            try
            {
                VehicleModel vehicle = await _vehicleService.GetVehicleWithAllData(Vin);
                if (vehicle == null)
                {
                    MessageBox.Show("There is no report for this vehicle VIN.", "No report", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Vin = string.Empty;
                }
                else
                {
                    _vehicleService.Vehicle = vehicle;
                    _eventAggregator.PublishOnUIThread(new MainNavigationMessage(MainNavigationMessages.VEHICLE_REPORT));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured while acquiring the report for this vehicle", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
