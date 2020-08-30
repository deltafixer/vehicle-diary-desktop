using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using VehicleDiary.Services;
using static VehicleDiary.Models.Enums.VehicleEnums;

namespace VehicleDiary.Main.ViewModels
{
    public class CreateSaleListingViewModel: Screen
    {
        private readonly SaleListingService _saleListingService;
        private string _price;
        private string _selectedCondition = Condition.USED.ToString();
        public List<string> Conditions
        {
            get => Enum.GetValues(typeof(Condition)).Cast<Condition>().Select(v => v.ToString()).ToList();
        }

        public CreateSaleListingViewModel(SaleListingService saleListingService)
        {
            _saleListingService = saleListingService;
        }

        public Make Make => _saleListingService.VehicleForNewSaleListing.Make;
        public Model Model => _saleListingService.VehicleForNewSaleListing.Model;
        public string Vin => _saleListingService.VehicleForNewSaleListing.Vin;
        public string Price
        {
            get => _price; set { _price = value; NotifyOfPropertyChange(() => Price); NotifyOfPropertyChange(() => CanSubmitListing); }
        }
        public string SelectedCondition { get => _selectedCondition; set => Set(ref _selectedCondition, value); }

        public bool CanSubmitListing => (Price?.Length > 0);

        public void SubmitListing()
        {

        }
    }
}
