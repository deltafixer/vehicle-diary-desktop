using Caliburn.Micro;
using VehicleDiary.Login.Messages;
using VehicleDiary.Main.Messages;

namespace VehicleDiary.Login.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;

        public LoginViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Login() => _eventAggregator.PublishOnUIThread(new AuthenticatedMessage());
        public void Register() => _eventAggregator.PublishOnUIThread(new RegisterUserMessage());
    }
}
