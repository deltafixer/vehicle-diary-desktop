using System;
using VehicleDiary.Models;
using static VehicleDiary.Models.Enums.VehicleEnums;

namespace VehicleDiary.Main.ViewModels
{
    public class SaleListingViewModel
    {
        private readonly SaleListingModel _saleListing;

        public SaleListingViewModel(SaleListingModel saleListing)
        {
            _saleListing = saleListing;
        }

        public Make Make => _saleListing.Vehicle.Make;
        public Model Model => _saleListing.Vehicle.Model;
        public float Price => _saleListing.Price;
        public DateTime DateAdded => _saleListing.DateAdded;
        public Condition Condition => _saleListing.Condition;
        public double SuggestionScore => _saleListing.SuggestionScore;
    }
}
