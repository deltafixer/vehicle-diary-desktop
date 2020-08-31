using Caliburn.Micro;
using VehicleDiary.Main.Messages;
using VehicleDiary.Models;
using VehicleDiary.Services;

namespace VehicleDiary.Main.ViewModels
{
    public class ProfilePanelViewModel : Screen, IHandle<DataMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly UserService _userService;
        private string _welcomeUserMessage;
        
        public UserModel User { get => _userService.User; }

        public string WelcomeUserMessage { get => "Welcome, " + _welcomeUserMessage; set => Set(ref _welcomeUserMessage, value); }

        public ProfilePanelViewModel(IEventAggregator eventAggregator, UserService userService)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _userService = userService;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        public void Handle(DataMessage message)
        {
            if (message.Message == DataMessages.USER)
            {
                WelcomeUserMessage = _userService.User.Username;
            }
        }
    }
}
