using Caliburn.Micro;
using VehicleDiary.Authenticate.Messages;

namespace VehicleDiary.Authenticate.ViewModels
{
    public class AuthenticationConductorViewModel : Conductor<Screen>.Collection.OneActive, IHandle<AuthenticationNavigationMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly LoginViewModel _loginViewModel;
        private readonly RegisterTypeViewModel _registerTypeViewModel;
        private readonly RegisterPersonViewModel _registerPersonViewModel;
        private readonly RegisterServiceViewModel _registerServiceViewModel;

        public AuthenticationConductorViewModel(IEventAggregator eventAggregator, LoginViewModel loginViewModel, RegisterTypeViewModel registerTypeViewModel, RegisterPersonViewModel registerPersonViewModel, RegisterServiceViewModel registerServiceViewModel)
        {
            _eventAggregator = eventAggregator;
            _loginViewModel = loginViewModel;
            _registerTypeViewModel = registerTypeViewModel;
            _registerPersonViewModel = registerPersonViewModel;
            _registerServiceViewModel = registerServiceViewModel;

            Items.AddRange(new Screen[] { _loginViewModel, _registerTypeViewModel, });
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            _eventAggregator.Subscribe(this);
            ActivateItem(_loginViewModel);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _eventAggregator.Unsubscribe(this);
        }

        public void Handle(AuthenticationNavigationMessage message)
        {
            switch (message.NavigateTo)
            {
                case AuthenticationNavigationMessages.LOGIN:
                    ActivateItem(_loginViewModel);
                    break;
                case AuthenticationNavigationMessages.REGISTER_TYPE:
                    ActivateItem(_registerTypeViewModel);
                    break;
                case AuthenticationNavigationMessages.REGISTER_PERSON:
                    ActivateItem(_registerPersonViewModel);
                    break;
                case AuthenticationNavigationMessages.REGISTER_SERVICE:
                    ActivateItem(_registerServiceViewModel);
                    break;
                default:
                    break;
            }
        }
    }
}
