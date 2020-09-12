using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VehicleDiary.Models;
using VehicleDiary.Services;
using static VehicleDiary.Models.Enums.UserEnums;

namespace VehicleDiary.Main.ViewModels
{
    using BCrypt = BCrypt.Net.BCrypt;
    public class PersonUserProfileViewModel : Screen
    {
        // COMMENT: this is not a representative of modern UX
        private readonly PersonUserService _personUserService;
        private readonly UniversalCRUDService<PersonUserModel> _personUserCrudService;
        private readonly UniversalCRUDService<UserModel> _userCrudService;
        private string _firstName;
        private string _lastName;
        private string _currentPassword;
        private string _password;
        private string _repeatPassword;
        private string _selectedPersonType;

        public PersonUserProfileViewModel(
            PersonUserService personUserService,
            UniversalCRUDService<UserModel> userCrudService,
            UniversalCRUDService<PersonUserModel> personUserCrudService
            )
        {
            _personUserService = personUserService;
            _userCrudService = userCrudService;
            _personUserCrudService = personUserCrudService;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            if (_personUserService.PersonUser != null)
            {
                FirstName = _personUserService.PersonUser.FirstName;
                LastName = _personUserService.PersonUser.LastName;
                SelectedPersonType = _personUserService.PersonUser.PersonType.ToString();
            }
        }

        public string FirstName
        {
            get => _firstName; set { _firstName = value; NotifyOfPropertyChange(() => FirstName); NotifyOfPropertyChange(() => CanSave); }
        }
        public string LastName
        {
            get => _lastName; set { _lastName = value; NotifyOfPropertyChange(() => LastName); NotifyOfPropertyChange(() => CanSave); }
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

        public List<string> PersonTypes
        {
            get => Enum.GetValues(typeof(PersonType)).Cast<PersonType>().Select(v => v.ToString()).ToList();
        }

        public string SelectedPersonType { get => _selectedPersonType; set => Set(ref _selectedPersonType, value); }

        public bool CanSave => (CurrentPassword?.Length > 0
            && BCrypt.Verify(CurrentPassword, _personUserService.PersonUser.User.Password)
            && FirstName?.Length > 0
            && LastName?.Length > 0
            && Password?.Length > 0
            && Password == RepeatPassword);

        public async void Save()
        {
            try
            {
                await _userCrudService.Update(new UserModel
                {
                    Username = _personUserService.PersonUser.User.Username,
                    Password = BCrypt.HashPassword(Password),
                    Role = Role.USER,
                    UserType = UserType.PERSON
                });
                await _personUserCrudService.Update(new PersonUserModel
                {
                    Id = _personUserService.PersonUser.Id,
                    FirstName = FirstName,
                    LastName = LastName,
                    PersonType = (PersonType)Enum.Parse(typeof(PersonType), SelectedPersonType)
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
