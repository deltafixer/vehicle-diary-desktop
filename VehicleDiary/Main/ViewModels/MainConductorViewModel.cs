using Caliburn.Micro;
using VehicleDiary.Main.Messages;

namespace VehicleDiary.Main.ViewModels
{
    public class MainConductorViewModel : Conductor<Screen>.Collection.OneActive, IHandle<MainNavigationMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly VinCheckViewModel _vinCheckViewModel;
        private readonly MyVehiclesViewModel _myVehiclesViewModel;
        private readonly MarketViewModel _marketViewModel;
        private readonly ProfileConductorViewModel _profileConductorViewModel;
        private readonly VehicleReportViewModel _vehicleReportViewModel;
        private readonly CreateSaleListingViewModel _createSaleListingViewModel;
        public HeaderViewModel HeaderViewModel { get; }
        public ProfilePanelViewModel ProfilePanelViewModel { get; }

        public MainConductorViewModel(IEventAggregator eventAggregator,
            HeaderViewModel headerViewModel,
            ProfilePanelViewModel profilePanelViewModel,
            VinCheckViewModel vinCheckViewModel,
            MyVehiclesViewModel myVehiclesViewModel,
            MarketViewModel marketViewModel,
            ProfileConductorViewModel profileConductorViewModel,
            VehicleReportViewModel vehicleReportViewModel,
            CreateSaleListingViewModel createSaleListingViewModel)
        {
            _eventAggregator = eventAggregator;
            HeaderViewModel = headerViewModel;
            ProfilePanelViewModel = profilePanelViewModel;
            _vinCheckViewModel = vinCheckViewModel;
            _myVehiclesViewModel = myVehiclesViewModel;
            _marketViewModel = marketViewModel;
            _profileConductorViewModel = profileConductorViewModel;
            _vehicleReportViewModel = vehicleReportViewModel;
            _createSaleListingViewModel = createSaleListingViewModel;

            //Items.AddRange(new Screen[] { _vinCheckViewModel });
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            _eventAggregator.Subscribe(this);
            ActivateItem(_vinCheckViewModel);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _eventAggregator.Unsubscribe(this);
        }

        public void Handle(MainNavigationMessage message)
        {
            switch (message.NavigateTo)
            {
                case MainNavigationMessages.CHECK_VIN:
                    ActivateItem(_vinCheckViewModel);
                    break;
                case MainNavigationMessages.MY_VEHICLES:
                    ActivateItem(_myVehiclesViewModel);
                    break;
                case MainNavigationMessages.MARKET:
                    ActivateItem(_marketViewModel);
                    break;
                case MainNavigationMessages.PROFILE:
                    ActivateItem(_profileConductorViewModel);
                    break;
                case MainNavigationMessages.VEHICLE_REPORT:
                    ActivateItem(_vehicleReportViewModel);
                    break;
                case MainNavigationMessages.VEHICLE_SALE_LISTING:
                    ActivateItem(_createSaleListingViewModel);
                    break;
                default:
                    break;
            }
        }
    }
}
