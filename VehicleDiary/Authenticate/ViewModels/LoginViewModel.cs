using Caliburn.Micro;
using System;
using VehicleDiary.Authenticate.Messages;
using VehicleDiary.Models;
using VehicleDiary.Services;

namespace VehicleDiary.Authenticate.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private string _username;
        private string _password;
        private readonly VehicleDiaryDbContext _vehicleDiaryDbContext;
        private readonly UniversalCRUDService<PersonUserModel> _personUserService;

        public string Username { get => _username; set => Set(ref _username, value, nameof(CanLogin)); }
        public string Password { get => _password; set => Set(ref _password, value, nameof(CanLogin)); }

        public LoginViewModel(IEventAggregator eventAggregator, VehicleDiaryDbContext vehicleDiaryDbContext)
        {
            _eventAggregator = eventAggregator;
            _vehicleDiaryDbContext = vehicleDiaryDbContext;
            _personUserService = new UniversalCRUDService<PersonUserModel>(_vehicleDiaryDbContext);
        }

        public bool CanLogin => (Username?.Length > 0 && Password?.Length > 0);

        //public async void Login() => await _personUserService.Create(new PersonUserModel { Username = "test2", Password = "test", Role = Models.Enums.UserEnums.Role.USER, FirstName = "fname", LastName = "lname", UserType = Models.Enums.UserEnums.UserType.PERSON });
        public async void Login() => await _personUserService.Delete("test2");

        public void Register() => _eventAggregator.PublishOnUIThread(new AuthenticationNavigationMessage(AuthenticationNavigationOptions.RegisterType));
    }
}
