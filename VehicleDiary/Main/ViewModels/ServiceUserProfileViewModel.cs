using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VehicleDiary.Models;
using VehicleDiary.Services;

namespace VehicleDiary.Main.ViewModels
{
    using static VehicleDiary.Models.Enums.UserEnums;
    using BCrypt = BCrypt.Net.BCrypt;
    public class ServiceUserProfileViewModel : Screen
    {
        private readonly ServiceUserService _serviceUserService;
        private readonly UniversalCRUDService<ServiceUserModel> _serviceUserCrudService;
        private readonly UniversalCRUDService<UserModel> _userCrudService;
        private string _name;
        private string _currentPassword;
        private string _password;
        private string _repeatPassword;
        private string _selectedServiceType;

        public ServiceUserProfileViewModel(
            ServiceUserService serviceUserService,
            UniversalCRUDService<UserModel> userCrudService,
            UniversalCRUDService<ServiceUserModel> serviceUserCrudService
        )
        {
            _serviceUserService = serviceUserService;
            _userCrudService = userCrudService;
            _serviceUserCrudService = serviceUserCrudService;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            if (_serviceUserService.ServiceUser != null)
            {
                Name = _serviceUserService.ServiceUser.Name;
                SelectedServiceType = _serviceUserService.ServiceUser.ServiceType.ToString();
            }
        }

        public string Name
        {
            get => _name; set { _name = value; NotifyOfPropertyChange(() => Name); NotifyOfPropertyChange(() => CanSave); }
        }

        public string CurrentPassword
        {
            get => _currentPassword; set { _currentPassword = value; NotifyOfPropertyChange(() => CurrentPassword); NotifyOfPropertyChange(() => CanSave); }
        }

        public string Password
        {
            get => _password; set { _password = value; NotifyOfPropertyChange(() => Password); NotifyOfPropertyChange(() => CanSave); }
        }
        public string RepeatPassword
        {
            get => _repeatPassword; set { _repeatPassword = value; NotifyOfPropertyChange(() => RepeatPassword); NotifyOfPropertyChange(() => CanSave); }
        }

        public List<string> ServiceTypes
        {
            get => Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>().Select(v => v.ToString()).ToList();
        }

        public string SelectedServiceType { get => _selectedServiceType; set => Set(ref _selectedServiceType, value); }

        public bool CanSave => (CurrentPassword?.Length > 0
            && BCrypt.Verify(CurrentPassword, _serviceUserService.ServiceUser.User.Password)
            && Name?.Length > 0
            && Password?.Length > 0
            && Password == RepeatPassword);

        public async void Save()
        {
            try
            {
                await _userCrudService.Update(new UserModel
                {
                    Username = _serviceUserService.ServiceUser.User.Username,
                    Password = BCrypt.HashPassword(Password),
                    Role = Role.USER,
                    UserType = UserType.PERSON
                });
                await _serviceUserCrudService.Update(new ServiceUserModel
                {
                    Id = _serviceUserService.ServiceUser.Id,
                    Name = Name,
                    ServiceType = (ServiceType)Enum.Parse(typeof(ServiceType), SelectedServiceType)
                });
                MessageBox.Show("Successfully updated profile data!", "Update successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured, please check the data You have entered.", "Update error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
