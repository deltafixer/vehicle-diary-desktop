using Caliburn.Micro;
using VehicleDiary.Login.Messages;

namespace VehicleDiary.Login.ViewModels
{
    public class LoginConductorViewModel : Conductor<Screen>.Collection.OneActive, IHandle<RegisterUserMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly LoginViewModel _loginViewModel;
        private readonly RegisterViewModel _registerViewModel;

        public LoginConductorViewModel(IEventAggregator eventAggregator, LoginViewModel loginViewModel, RegisterViewModel registerViewModel)
        {
            _eventAggregator = eventAggregator;
            _loginViewModel = loginViewModel;
            _registerViewModel = registerViewModel;

            Items.AddRange(new Screen[] { _loginViewModel, _registerViewModel });
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

        public void Handle(RegisterUserMessage message)
        {
            ActivateItem(_registerViewModel);
        }
    }
}
