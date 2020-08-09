using Caliburn.Micro;
using VehicleDiary.Authenticate.Messages;

namespace VehicleDiary.Authenticate.ViewModels
{
    public class RegisterPersonViewModel : Screen
    {
        private IEventAggregator _eventAggregator;

        public RegisterPersonViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Back() => _eventAggregator.PublishOnUIThread(new AuthenticationNavigationMessage(AuthenticationNavigationOptions.RegisterType));
    }
}