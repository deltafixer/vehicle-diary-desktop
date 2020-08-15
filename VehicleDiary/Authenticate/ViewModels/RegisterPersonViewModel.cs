using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VehicleDiary.Authenticate.Messages;
using VehicleDiary.Models;
using VehicleDiary.Services;
using static VehicleDiary.Models.Enums.UserEnums;

namespace VehicleDiary.Authenticate.ViewModels
{
using BCrypt = BCrypt.Net.BCrypt;
    public class RegisterPersonViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly UniversalCRUDService<PersonUserModel> _personUserService;
        private string _firstName;
        private string _lastName;
        private string _username;
        private string _password;
        private string _repeatPassword;
        private string _selectedUserType = UserType.PERSON.ToString();

        public RegisterPersonViewModel(IEventAggregator eventAggregator, UniversalCRUDService<PersonUserModel> personUserService)
        {
            _eventAggregator = eventAggregator;
            _personUserService = personUserService;
        }

        public string FirstName
        {
            get => _firstName; set { _firstName = value; NotifyOfPropertyChange(() => FirstName); NotifyOfPropertyChange(() => CanRegister); }
        }
        public string LastName
        {
            get => _lastName; set { _lastName = value; NotifyOfPropertyChange(() => LastName); NotifyOfPropertyChange(() => CanRegister); }
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

        public List<string> UserTypes
        {
            get => Enum.GetValues(typeof(UserType)).Cast<UserType>().Select(v => v.ToString()).ToList();
        }

        public string SelectedUserType { get => _selectedUserType; set => Set(ref _selectedUserType, value); }

        public bool CanRegister => (FirstName?.Length > 0 && LastName?.Length > 0 && Username?.Length > 0 && Password?.Length > 0 && Password == RepeatPassword);

        public void Back() => _eventAggregator.PublishOnUIThread(new AuthenticationNavigationMessage(AuthenticationNavigationOptions.RegisterType));

        public async Task Register()
        {
            try
            {
                await _personUserService.Create(new PersonUserModel { FirstName = FirstName, LastName = LastName, Username = Username, Password = BCrypt.HashPassword(Password), UserType = (UserType)Enum.Parse(typeof(UserType), SelectedUserType), Role = Role.USER }).ConfigureAwait(continueOnCapturedContext: true); ;

                if (MessageBox.Show("Successfully created a person account.\nUse Your new credentials to login!", "Registration successful", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    ClearFields();
                    _eventAggregator.PublishOnUIThread(new AuthenticationNavigationMessage(AuthenticationNavigationOptions.Login));
                }
            }
            catch (Exception)
            {
                if (MessageBox.Show("Please use a different username.", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    Username = string.Empty;
                }
                // debug SQL error code
                //foreach (var eve in e.EntityValidationErrors)
                //{
                //    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                //            ve.PropertyName, ve.ErrorMessage);
                //    }
                //}
            }
        }

        public void ClearFields() => FirstName = LastName = Username = Password = RepeatPassword = string.Empty;
    }
}