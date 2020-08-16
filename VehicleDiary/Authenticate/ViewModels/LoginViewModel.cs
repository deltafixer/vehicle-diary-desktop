using Caliburn.Micro;
using System;
using System.Threading.Tasks;
using System.Windows;
using VehicleDiary.Authenticate.Messages;
using VehicleDiary.Models;
using VehicleDiary.Services;

namespace VehicleDiary.Authenticate.ViewModels
{
    using BCrypt = BCrypt.Net.BCrypt;
    public class LoginViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly UserService _userService;
        private string _username;
        private string _password;

        public string Username
        {
            get => _username; set { _username = value; NotifyOfPropertyChange(() => Username); NotifyOfPropertyChange(() => CanLogin); }
        }
        public string Password
        {
            get => _password; set { _password = value; NotifyOfPropertyChange(() => Password); NotifyOfPropertyChange(() => CanLogin); }
        }

        public LoginViewModel(IEventAggregator eventAggregator, UserService userService)
        {
            _eventAggregator = eventAggregator;
            _userService = userService;
        }

        public bool CanLogin => (Username?.Length > 0 && Password?.Length > 0);

        public async Task Login()
        {
            UserModel user = await _userService.CheckCredentials(Username, BCrypt.HashPassword(Password));
            if (user != null && BCrypt.Verify(Password, user.Password))
            {
                MessageBox.Show("Good credentials!");
            }
            else
            {
                MessageBox.Show("You have entered incorrect credentials, please try again.", "Wrong credentials", MessageBoxButton.OK, MessageBoxImage.Warning);
                ClearFields();
            }
        }

        public void Register() => _eventAggregator.PublishOnUIThread(new AuthenticationNavigationMessage(AuthenticationNavigationOptions.RegisterType));

        public void ClearFields() => Username = Password = string.Empty;
    }
}
