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
        public HeaderViewModel HeaderViewModel { get; }
        public ProfilePanelViewModel ProfilePanelViewModel { get; }

        public MainConductorViewModel(IEventAggregator eventAggregator, HeaderViewModel headerViewModel, VinCheckViewModel vinCheckViewModel, MyVehiclesViewModel myVehiclesViewModel, ProfilePanelViewModel profilePanelViewModel, MarketViewModel marketViewModel)
        {
            _eventAggregator = eventAggregator;
            HeaderViewModel = headerViewModel;
            ProfilePanelViewModel = profilePanelViewModel;
            _vinCheckViewModel = vinCheckViewModel;
            _myVehiclesViewModel = myVehiclesViewModel;
            _marketViewModel = marketViewModel;

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
                default:
                    break;
            }
        }
    }
}
