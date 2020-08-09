using Caliburn.Micro;
using System;
using VehicleDiary.Authenticate.Messages;

namespace VehicleDiary.Authenticate.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private string _password;

        public string Password
        {
            get => _password;
            // to notify UI on changes
            set => Set(ref _password, value);
        }

        public LoginViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Login() => Console.WriteLine("TODO");

        public void Register() => _eventAggregator.PublishOnUIThread(new AuthenticationNavigationMessage(AuthenticationNavigationOptions.RegisterType));
    }
}
