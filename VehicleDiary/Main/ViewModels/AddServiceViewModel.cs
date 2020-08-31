using Caliburn.Micro;
using System;
using System.Windows;
using VehicleDiary.Models;
using VehicleDiary.Services;

namespace VehicleDiary.Main.ViewModels
{
    public class AddServiceViewModel : Screen
    {
        private readonly VehicleService _vehicleService;
        private readonly ServiceUserService _serviceUserService;
        private string _vin;
        private DateTime _dateTaken = DateTime.Now;
        private string _price;
        private string _serviceDetails;

        public AddServiceViewModel(VehicleService vehicleService, ServiceUserService serviceUserService)
        {
            _vehicleService = vehicleService;
            _serviceUserService = serviceUserService;
        }

        public string Vin
        {
            get => _vin; set { _vin = value; NotifyOfPropertyChange(() => Vin); NotifyOfPropertyChange(() => CanSubmitService); }
        }
        public DateTime DateTaken
        {
            get => _dateTaken; set { _dateTaken = value; NotifyOfPropertyChange(() => DateTaken); NotifyOfPropertyChange(() => CanSubmitService); }
        }
        public string Price
        {
            get => _price; set { _price = value; NotifyOfPropertyChange(() => Price); NotifyOfPropertyChange(() => CanSubmitService); }
        }
        public string ServiceDetails
        {
            get => _serviceDetails; set { _serviceDetails = value; NotifyOfPropertyChange(() => ServiceDetails); NotifyOfPropertyChange(() => CanSubmitService); }
        }

        public bool CanSubmitService => (Vin?.Length == 17 && Price?.Length > 0 && ServiceDetails?.Length > 0);

        public async void SubmitService()
        {
            try
            {
                VehicleModel vehicle = await _vehicleService.GetVehicle(Vin);
                ServiceUserModel serviceUser = await _vehicleService.GetServiceUser(_serviceUserService.ServiceUser.Id);
                if (vehicle != null)
                {
                    await _vehicleService.CreateVehicleService(new VehicleServiceModel { Vehicle = vehicle, ServicedBy = serviceUser, DateTaken = DateTaken, Price = float.Parse(Price), ServiceDetails = ServiceDetails });
                    MessageBox.Show("Successfully created a vehicle service!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Vehicle with this VIN does not exist in the database.", "No Vehicle", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Vin = string.Empty;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured while creating a service for this vehicle", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            Vin = Price = ServiceDetails = string.Empty;
        }
    }
}
