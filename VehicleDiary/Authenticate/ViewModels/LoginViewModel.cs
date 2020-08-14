using Caliburn.Micro;
using System;
using VehicleDiary.Authenticate.Messages;

namespace VehicleDiary.Authenticate.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private string _username;
        private string _password;

        public string Username { get => _username; set => Set(ref _username, value, nameof(CanLogin)); }
        public string Password { get => _password; set => Set(ref _password, value, nameof(CanLogin)); }

        public LoginViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public bool CanLogin => (Username?.Length > 0 && Password?.Length > 0);

        public void Login() => Console.WriteLine("");

        public void Register() => _eventAggregator.PublishOnUIThread(new AuthenticationNavigationMessage(AuthenticationNavigationOptions.RegisterType));
    }
}
