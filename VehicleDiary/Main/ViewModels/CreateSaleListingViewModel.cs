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
        private string _price;
        private string _selectedCondition = Condition.USED.ToString();
        public List<string> Conditions
        {
            get => Enum.GetValues(typeof(Condition)).Cast<Condition>().Select(v => v.ToString()).ToList();
        }

        public CreateSaleListingViewModel(IEventAggregator eventAggregator, VehicleService vehicleService)
        {
            _eventAggregator = eventAggregator;
            _vehicleService = vehicleService;
        }

        public Make Make => _vehicleService.VehicleForSaleListing.Make;
        public Model Model => _vehicleService.VehicleForSaleListing.Model;
        public string Vin => _vehicleService.VehicleForSaleListing.Vin;
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
                VehicleModel vehicle = await _vehicleService.GetVehicle(_vehicleService.VehicleForSaleListing.Vin);
                SaleListingModel saleListing = await _vehicleService.CreateSaleListing(new SaleListingModel { Vehicle = vehicle, Price = float.Parse(Price), DateAdded = DateTime.Now, Condition = (Condition)Enum.Parse(typeof(Condition), SelectedCondition) });
                saleListing.Vehicle = vehicle;
                double score = await _vehicleService.GetSaleListingScore(saleListing.Id);
                saleListing.SuggestionScore = score;

                if (_vehicleService.saleListingsLoaded)
                {
                    _vehicleService.SaleListings.Add(saleListing);
                }
                
                _eventAggregator.PublishOnUIThread(new DataMessage(DataMessages.SALE_LISTING_CREATED, saleListing));
                
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
