using Caliburn.Micro;
using VehicleDiary.Main.Messages;

namespace VehicleDiary.Main.ViewModels
{
    public class MainConductorViewModel : Conductor<Screen>.Collection.OneActive, IHandle<NavigationMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly VinCheckViewModel _vinCheckViewModel;
        public HeaderViewModel HeaderViewModel { get; }
        public ProfilePanelViewModel ProfilePanelViewModel { get; }

        public MainConductorViewModel(IEventAggregator eventAggregator, HeaderViewModel headerViewModel, VinCheckViewModel homeViewModel, ProfilePanelViewModel profilePanelViewModel)
        {
            _eventAggregator = eventAggregator;
            _vinCheckViewModel = homeViewModel;
            HeaderViewModel = headerViewModel;
            ProfilePanelViewModel = profilePanelViewModel;

            Items.AddRange(new Screen[] { _vinCheckViewModel });
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

        public void Handle(NavigationMessage message)
        {
            switch (message.NavigateTo)
            {
                case NavigationMessages.MAIN:
                    ActivateItem(_vinCheckViewModel);
                    break;
                default:
                    break;
            }
        }
    }
}
