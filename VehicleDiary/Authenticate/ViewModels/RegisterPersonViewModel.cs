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
        private string _selectedPersonType = PersonType.INDIVIDUAL.ToString();

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

        public List<string> PersonTypes
        {
            get => Enum.GetValues(typeof(PersonType)).Cast<PersonType>().Select(v => v.ToString()).ToList();
        }

        public string SelectedPersonType { get => _selectedPersonType; set => Set(ref _selectedPersonType, value); }

        public bool CanRegister => (FirstName?.Length > 0 && LastName?.Length > 0 && Username?.Length > 0 && Password?.Length > 0 && Password == RepeatPassword);

        public void Back() => _eventAggregator.PublishOnUIThread(new AuthenticationNavigationMessage(AuthenticationNavigationMessages.REGISTER_TYPE));

        public async Task Register()
        {
            try
            {
                UserModel user = new UserModel { Username = Username, Password = BCrypt.HashPassword(Password), Role = Role.USER, UserType = UserType.PERSON };
                await _personUserService.Create(new PersonUserModel { User = user, FirstName = FirstName, LastName = LastName, PersonType = (PersonType)Enum.Parse(typeof(PersonType), SelectedPersonType) });

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