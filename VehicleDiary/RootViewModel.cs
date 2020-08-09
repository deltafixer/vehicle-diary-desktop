using Caliburn.Micro;
using VehicleDiary.Authenticate.ViewModels;
using VehicleDiary.Main.Messages;
using VehicleDiary.Main.ViewModels;

namespace VehicleDiary.ViewModels
{
    public class RootViewModel : Conductor<Screen>.Collection.OneActive, IHandle<NavigationMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly AuthenticationConductorViewModel _authenticationConductorViewModel;
        private readonly MainConductorViewModel _mainConductorViewModel;

        public RootViewModel(IEventAggregator eventAggregator, AuthenticationConductorViewModel authenticationConductorViewModel, MainConductorViewModel mainConductorViewModel)
        {
            _eventAggregator = eventAggregator;
            _authenticationConductorViewModel = authenticationConductorViewModel;
            _mainConductorViewModel = mainConductorViewModel;

            Items.AddRange(new Screen[] { _authenticationConductorViewModel, _mainConductorViewModel });
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            _eventAggregator.Subscribe(this);
            ActivateItem(_authenticationConductorViewModel);
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
                case NavigationOptions.Home:
                    ActivateItem(_mainConductorViewModel);
                    break;
                case NavigationOptions.Authentication:
                    ActivateItem(_authenticationConductorViewModel);
                    break;
                default:
                    break;
            }
        }
    }
}
