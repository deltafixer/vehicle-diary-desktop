using Caliburn.Micro;
using VehicleDiary.Authenticate.Messages;

namespace VehicleDiary.Authenticate.ViewModels
{
    public class RegisterTypeViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private bool _personTypeIsChecked = true;
        private bool _serviceTypeIsChecked;

        public RegisterTypeViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public bool PersonTypeIsChecked
        {
            get => _personTypeIsChecked;
            set => Set(ref _personTypeIsChecked, value);
        }

        public bool ServiceTypeIsChecked
        {
            get => _serviceTypeIsChecked;
            set => Set(ref _serviceTypeIsChecked, value);
        }

        public void Continue() => _eventAggregator.PublishOnUIThread(new AuthenticationNavigationMessage(PersonTypeIsChecked ? AuthenticationNavigationOptions.RegisterPerson : AuthenticationNavigationOptions.RegisterService));

        public void Back() => _eventAggregator.PublishOnUIThread(new AuthenticationNavigationMessage(AuthenticationNavigationOptions.Login));
    }
}
