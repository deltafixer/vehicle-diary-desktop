using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VehicleDiary.Main.Messages;
using VehicleDiary.Models;
using VehicleDiary.Services;
using static VehicleDiary.Models.Enums.VehicleEnums;
using Condition = VehicleDiary.Models.Enums.VehicleEnums.Condition;

namespace VehicleDiary.Main.ViewModels
{
    public class CreateSaleListingViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly VehicleService _vehicleService;
        private readonly SaleListingService _saleListingService;
        private string _price;
        private string _selectedCondition = Condition.USED.ToString();
        public List<string> Conditions
        {
            get => Enum.GetValues(typeof(Condition)).Cast<Condition>().Select(v => v.ToString()).ToList();
        }

        public CreateSaleListingViewModel(IEventAggregator eventAggregator, VehicleService vehicleService, SaleListingService saleListingService)
        {
            _eventAggregator = eventAggregator;
            _vehicleService = vehicleService;
            _saleListingService = saleListingService;
        }

        public Make Make => _vehicleService.VehicleForNewSaleListing.Make;
        public Model Model => _vehicleService.VehicleForNewSaleListing.Model;
        public string Vin => _vehicleService.VehicleForNewSaleListing.Vin;
        public string Price
        {
            get => _price; set { _price = value; NotifyOfPropertyChange(() => Price); NotifyOfPropertyChange(() => CanSubmitListing); }
        }
        public string SelectedCondition { get => _selectedCondition; set => Set(ref _selectedCondition, value); }

        public bool CanSubmitListing => (Price?.Length > 0);

        public async void SubmitListing()
        {
            try
            {
                VehicleModel vehicle = await _vehicleService.GetVehicle(_vehicleService.VehicleForNewSaleListing.Vin);
                SaleListingModel saleListing = await _vehicleService.CreateSaleListing(new SaleListingModel { Vehicle = vehicle, Price = float.Parse(Price), DateAdded = DateTime.Now, Condition = (Condition)Enum.Parse(typeof(Condition), SelectedCondition) });
                _saleListingService.SaleListings.Add(saleListing);
                if (MessageBox.Show("Successfully created a sale listing!", "Success", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    _eventAggregator.PublishOnUIThread(new MainNavigationMessage(MainNavigationMessages.MARKET));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("A sale listing for this vehicle already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
