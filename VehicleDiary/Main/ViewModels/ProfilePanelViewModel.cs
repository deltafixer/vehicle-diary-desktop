using VehicleDiary.Models;
using VehicleDiary.Services;

namespace VehicleDiary.Main.ViewModels
{
    public class ProfilePanelViewModel
    {
        private readonly UserService _userService;
        public UserModel User { get => _userService.User; }
        public string WelcomeUserMessage => "Welcome, " + _userService.User?.Username;

        public ProfilePanelViewModel(UserService userService)
        {
            _userService = userService;
        }
    }
}
