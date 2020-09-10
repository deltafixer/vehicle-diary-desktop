using Caliburn.Micro;
using System;
using System.Threading.Tasks;
using System.Windows;
using VehicleDiary.Main.Messages;
using VehicleDiary.Models;
using VehicleDiary.Services;
using static VehicleDiary.Models.Enums.VehicleEnums;

namespace VehicleDiary.Main.ViewModels
{
    public class SaleListingViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly SaleListingModel _saleListing;
        private readonly PersonUserService _personUserService;
        private readonly VehicleService _vehicleService;
        public event EventHandler SaleListingRemoved;

        public SaleListingViewModel(
            IEventAggregator eventAggregator,
            SaleListingModel saleListing,
            PersonUserService personUserService,
            VehicleService vehicleService)
        {
            _eventAggregator = eventAggregator;
            _saleListing = saleListing;
            _personUserService = personUserService;
            _vehicleService = vehicleService;
        }

        public int Id => _saleListing.Id;
        public Make Make => _saleListing.Vehicle.Make;
        public Model Model => _saleListing.Vehicle.Model;
        public float Price => _saleListing.Price;
        public DateTime DateAdded => _saleListing.DateAdded;
        public Enums.VehicleEnums.Condition Condition => _saleListing.Condition;
        public double SuggestionScore => _saleListing.SuggestionScore;
        public string ControlsVisibility
        {
            get
            {
                foreach (VehicleModel vehicle in _personUserService.Vehicles)
                {
                    if (vehicle.Vin == _saleListing.Vehicle.Vin)
                    {
                        return "Visible";
                    }
                }
                return "Collapsed";
            }
        }
        public VehicleModel Vehicle { get => _saleListing.Vehicle; }

        public async Task RemoveListing()
        {
            if (MessageBox.Show("Are you sure you want to delete this sale listing?", "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                VehicleModel vehicle = _saleListing.Vehicle;
                await _vehicleService.DeleteSaleListing(_saleListing);
                _saleListing.Vehicle = vehicle;
                _eventAggregator.PublishOnUIThread(new DataMessage(DataMessages.SALE_LISTING_REMOVED, _saleListing));
                SaleListingRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
