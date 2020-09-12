using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VehicleDiary.Main.Messages;
using VehicleDiary.Models;
using VehicleDiary.Services;

namespace VehicleDiary.Main.ViewModels
{
    public class MarketViewModel : Screen, IHandle<DataMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly PersonUserService _personUserService;
        private readonly VehicleService _vehicleService;
        public BindableCollection<SaleListingViewModel> SaleListings { get; set; }
        private int _saleListingsCount;

        public int SaleListingsCount
        {
            get => _saleListingsCount; set { _saleListingsCount = value; NotifyOfPropertyChange(() => SaleListingsCount); }
        }

        public MarketViewModel(IEventAggregator eventAggregator,
            PersonUserService personUserService,
            VehicleService vehicleService)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _personUserService = personUserService;
            _vehicleService = vehicleService;
            SaleListings = new BindableCollection<SaleListingViewModel>();
        }

        private SaleListingViewModel CreateSaleListingViewModel(SaleListingModel saleListingModel)
        {
            SaleListingViewModel saleListingViewModel = new SaleListingViewModel(_eventAggregator, saleListingModel, _personUserService, _vehicleService);
            saleListingViewModel.SaleListingRemoved += OnSaleListingRemoved;
            return saleListingViewModel;
        }

        protected override async void OnActivate()
        {
            base.OnActivate();
            _vehicleService.SaleListings.Clear();
            SaleListings.Clear();
            IEnumerable<SaleListingModel> saleListings = await _vehicleService.GetSaleListingsWithVehicles();
            if (saleListings != null)
            {
                foreach (SaleListingModel saleListing in saleListings)
                {
                    double score = await _vehicleService.GetSaleListingScore(saleListing.Id);
                    saleListing.SuggestionScore = score;
                }
                _vehicleService.SaleListings.AddRange(saleListings);
                SaleListings.AddRange(_vehicleService.SaleListings.Select(saleListing => CreateSaleListingViewModel(saleListing)));
                SaleListingsCount = SaleListings.Count;
            }
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            UnsubscribeAll();
        }

        ~MarketViewModel()
        {
            UnsubscribeAll();
        }

        private void UnsubscribeAll()
        {
            foreach (SaleListingViewModel saleListing in SaleListings)
            {
                saleListing.SaleListingRemoved -= OnSaleListingRemoved;
            }
        }

        private void OnSaleListingRemoved(object sender, EventArgs e)
        {
            SaleListingViewModel saleListingToRemove = (SaleListingViewModel)sender;
            saleListingToRemove.SaleListingRemoved -= OnSaleListingRemoved;
            SaleListings.Remove(saleListingToRemove);
            MessageBox.Show("Successfully removed the sale listing!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void Handle(DataMessage message)
        {
            if (message.Message == DataMessages.CLEAR_ALL)
            {
                _vehicleService.SaleListings.Clear();
                SaleListings.Clear();
            }
        }
    }
}
