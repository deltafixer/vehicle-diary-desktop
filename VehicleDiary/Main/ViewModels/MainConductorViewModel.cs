using Caliburn.Micro;
using VehicleDiary.Main.Messages;

namespace VehicleDiary.Main.ViewModels
{
    public class MainConductorViewModel : Conductor<Screen>.Collection.OneActive, IHandle<NavigateMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly HomeViewModel _homeViewModel;
        public MainConductorViewModel(IEventAggregator eventAggregator, HeaderViewModel headerViewModel, HomeViewModel homeViewModel)
        {
            _eventAggregator = eventAggregator;
            HeaderViewModel = headerViewModel;
            _homeViewModel = homeViewModel;

            Items.AddRange(new Screen[] { _homeViewModel });
        }

        public HeaderViewModel HeaderViewModel { get; }

        protected override void OnActivate()
        {
            base.OnActivate();
            _eventAggregator.Subscribe(this);
            ActivateItem(_homeViewModel);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _eventAggregator.Unsubscribe(this);
        }

        public void Handle(NavigateMessage message)
        {
            switch (message.NavigateTo)
            {
                case NavigationOptions.Home:
                    ActivateItem(_homeViewModel);
                    break;
                default:
                    break;
            }

        }
    }
}
