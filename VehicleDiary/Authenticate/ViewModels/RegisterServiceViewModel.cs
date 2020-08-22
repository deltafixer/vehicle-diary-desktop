using Caliburn.Micro;
using System;
using System.Linq;
using System.Collections.Generic;
using static VehicleDiary.Models.Enums.UserEnums;
using VehicleDiary.Authenticate.Messages;
using VehicleDiary.Services;
using VehicleDiary.Models;
using System.Windows;
using System.Threading.Tasks;

namespace VehicleDiary.Authenticate.ViewModels
{
    using BCrypt = BCrypt.Net.BCrypt;
    public class RegisterServiceViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly UniversalCRUDService<ServiceUserModel> _serviceUserService;
        private string _name;
        private string _username;
        private string _password;
        private string _repeatPassword;
        private string _selectedServiceType = ServiceType.AUTHORIZED.ToString();

        public RegisterServiceViewModel(IEventAggregator eventAggregator, UniversalCRUDService<ServiceUserModel> serviceUserService)
        {
            _eventAggregator = eventAggregator;
            _serviceUserService = serviceUserService;
        }

        public string Name
        {
            get => _name; set { _name = value; NotifyOfPropertyChange(() => Name); NotifyOfPropertyChange(() => CanRegister); }
        }
        public string Username
        {
            get => _username; set { _username = value; NotifyOfPropertyChange(() => Username); NotifyOfPropertyChange(() => CanRegister); }
        }
        public string Password
        {
            get => _password; set { _password = value; NotifyOfPropertyChange(() => Password); NotifyOfPropertyChange(() => CanRegister); }
        }
        public string RepeatPassword
        {
            get => _repeatPassword; set { _repeatPassword = value; NotifyOfPropertyChange(() => RepeatPassword); NotifyOfPropertyChange(() => CanRegister); }
        }

        public List<string> ServiceTypes
        {
            get => Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>().Select(v => v.ToString()).ToList();
        }

        public string SelectedServiceType { get => _selectedServiceType; set => Set(ref _selectedServiceType, value); }

        public bool CanRegister => (Name?.Length > 0 && Username?.Length > 0 && Password?.Length > 0 && Password == RepeatPassword);

        public void Back() => _eventAggregator.PublishOnUIThread(new AuthenticationNavigationMessage(AuthenticationNavigationMessages.REGISTER_TYPE));
        public async Task Register()
        {
            try
            {
                UserModel user = new UserModel { Username = Username, Password = BCrypt.HashPassword(Password), Role = Role.USER, UserType = UserType.SERVICE};
                await _serviceUserService.Create(new ServiceUserModel { User = user, Name = Name, ServiceType = (ServiceType)Enum.Parse(typeof(ServiceType), SelectedServiceType) });

                if (MessageBox.Show("Successfully created a person account.\nUse Your new credentials to login!", "Registration successful", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    ClearFields();
                    _eventAggregator.PublishOnUIThread(new AuthenticationNavigationMessage(AuthenticationNavigationMessages.LOGIN));
                }
            }
            catch (Exception)
            {
                if (MessageBox.Show("Please use a different username.", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    Username = string.Empty;
                }
            }
        }
        public void ClearFields() => Name = Username = Password = RepeatPassword = string.Empty;
    }
}