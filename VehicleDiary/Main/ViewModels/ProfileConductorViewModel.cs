using Caliburn.Micro;
using VehicleDiary.Main.Messages;
using VehicleDiary.Services;
using static VehicleDiary.Models.Enums.UserEnums;

namespace VehicleDiary.Main.ViewModels
{
    public class ProfileConductorViewModel : Conductor<Screen>.Collection.OneActive, IHandle<DataMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly UserService _userService;
        private readonly PersonUserProfileViewModel _personUserProfileViewModel;
        private readonly ServiceUserProfileViewModel _serviceUserProfileViewModel;

        public ProfileConductorViewModel(
            IEventAggregator eventAggregator,
            UserService userService,
            PersonUserProfileViewModel personUserProfileViewModel,
            ServiceUserProfileViewModel serviceUserProfileViewModel)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _userService = userService;
            _personUserProfileViewModel = personUserProfileViewModel;
            _serviceUserProfileViewModel = serviceUserProfileViewModel;

            Items.AddRange(new Screen[] { _personUserProfileViewModel, _serviceUserProfileViewModel });
        }

        public void Handle(DataMessage dataMessage)
        {
            if (dataMessage.Message == DataMessages.USER)
            {
                switch (_userService.User.UserType)
                {
                    case UserType.PERSON:
                        ActivateItem(_personUserProfileViewModel);
                        break;
                    case UserType.SERVICE:
                        ActivateItem(_serviceUserProfileViewModel);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
