using Caliburn.Micro;
using System.Collections.Generic;
using VehicleDiary.Models;
using VehicleDiary.Services;

namespace VehicleDiary.Main.ViewModels
{
    public class MarketViewModel : Screen
    {
        private readonly SaleListingService _saleListingService;
        public BindableCollection<SaleListingViewModel> SaleListings { get; set; }
        public int SaleListingsCount => SaleListings.Count;

        public MarketViewModel(SaleListingService saleListingService)
        {
            _saleListingService = saleListingService;
            SaleListings = new BindableCollection<SaleListingViewModel>();
        }

        protected override async void OnActivate()
        {
            base.OnActivate();
            IEnumerable<SaleListingModel> saleListings = await _saleListingService.GetSaleListingsWithVehicles();
            if (saleListings != null)
            {
                _saleListingService.SaleListings.AddRange(saleListings);
            }
        }
    }
}
