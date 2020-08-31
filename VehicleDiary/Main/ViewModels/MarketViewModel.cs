using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using VehicleDiary.Models;
using VehicleDiary.Services;

namespace VehicleDiary.Main.ViewModels
{
    public class MarketViewModel : Screen
    {
        private readonly SaleListingService _saleListingService;
        public BindableCollection<SaleListingViewModel> SaleListings { get; set; }
        private int _saleListingsCount;
        public int SaleListingsCount
        {
            get => _saleListingsCount; set { _saleListingsCount = value; NotifyOfPropertyChange(() => SaleListingsCount); }
        }

        public MarketViewModel(SaleListingService saleListingService)
        {
            _saleListingService = saleListingService;
            SaleListings = new BindableCollection<SaleListingViewModel>();
        }

        protected override async void OnActivate()
        {
            base.OnActivate();
            if (_saleListingService.SaleListings.Count == 0)
            {
                IEnumerable<SaleListingModel> saleListings = await _saleListingService.GetSaleListingsWithVehicles();
                if (saleListings != null)
                {
                    foreach(SaleListingModel saleListing in saleListings)
                    {
                        double score = await _saleListingService.GetSaleListingScore(saleListing.Id);
                        saleListing.SuggestionScore = score;
                    }
                    _saleListingService.SaleListings.AddRange(saleListings);
                    SaleListings.AddRange(_saleListingService.SaleListings.Select(saleListing => new SaleListingViewModel(saleListing)));
                    SaleListingsCount = SaleListings.Count;
                }
            }
            else
            {
                SaleListings.AddRange(_saleListingService.SaleListings.Select(saleListing => new SaleListingViewModel(saleListing)));
                SaleListingsCount = SaleListings.Count;
            }
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            SaleListings.Clear();
        }
    }
}
