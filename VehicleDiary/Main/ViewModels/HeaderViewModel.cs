using Caliburn.Micro;
using System.Windows.Input;
using VehicleDiary.Main.Messages;
using VehicleDiary.Models;
using VehicleDiary.Services;
using VehicleDiary.Utilities;
using static VehicleDiary.Models.Enums.UserEnums;

namespace VehicleDiary.Main.ViewModels
{
    public class CustomMenuItem
    {
        public string Name { get; set; }
        public ICommand OnClick { get; set; }
    };
    public class HeaderViewModel : IHandle<DataMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly UserService _userService;
        private readonly PersonUserService _personUserService;
        private readonly UniversalCRUDService<PersonUserModel> _personUserCRUDService;
        public BindableCollection<CustomMenuItem> MenuItems { get; set; }

        public HeaderViewModel(IEventAggregator eventAggregator, UserService userService, PersonUserService personUserService, UniversalCRUDService<PersonUserModel> personUserCRUDService)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _userService = userService;
            _personUserService = personUserService;
            _personUserCRUDService = personUserCRUDService;
            MenuItems = new BindableCollection<CustomMenuItem>();
        }

        public async void Handle(DataMessage dataMessage)
        {
            switch (dataMessage.Message)
            {
                case DataMessages.USER:
                    switch (_userService.User.UserType)
                    {
                        case UserType.PERSON:
                            // COMMENT: Profile and Logout buttons are defined in the View. They are 'static' elements visible in all cases.
                            MenuItems.AddRange(new BindableCollection<CustomMenuItem>
                            {
                                new CustomMenuItem { Name = "Check VIN", OnClick = GoToCheckVin},
                                new CustomMenuItem { Name = "My vehicles", OnClick = GoToMyVehicles},
                                new CustomMenuItem { Name = "Market", OnClick = GoToMarket },
                            });
                            PersonUserModel personUser = await _personUserService.Get(_userService.User.Username);
                            _personUserService.PersonUser = personUser;
                            if (personUser.PersonType == PersonType.POLICE)
                            {
                                MenuItems.Insert(3, new CustomMenuItem { Name = "Report accident" });
                            }
                            break;
                        case UserType.SERVICE:
                            MenuItems.AddRange(new BindableCollection<CustomMenuItem>
                            {
                                new CustomMenuItem { Name = "Services" },
                            });
                            break;
                        default:
                            break;
                    }
                    // COMMENT: to avoid unsafe async reach to context from both this and MyVehiclesViewModel
                    _eventAggregator.PublishOnUIThread(new DataMessage(DataMessages.HEADER_LOADED));
                    break;
                default:
                    break;
            }
        }

        public ICommand GoToCheckVin
        {
            get
            {
                return new CommandHandler(() => ActionGoToCheckVin());
            }
        }
        public ICommand GoToMyVehicles
        {
            get
            {
                return new CommandHandler(() => ActionGoToMyVehicles());
            }
        }
        public ICommand GoToMarket
        {
            get
            {
                return new CommandHandler(() => ActionGoToMarket());
            }
        }
        public void ActionGoToCheckVin()
        {
            _eventAggregator.PublishOnUIThread(new MainNavigationMessage(MainNavigationMessages.CHECK_VIN));
        }

        public void ActionGoToMyVehicles()
        {
            _eventAggregator.PublishOnUIThread(new MainNavigationMessage(MainNavigationMessages.MY_VEHICLES));
        }

        public void ActionGoToMarket()
        {
            _eventAggregator.PublishOnUIThread(new MainNavigationMessage(MainNavigationMessages.MARKET));
        }

        public void GoToProfile() => _eventAggregator.PublishOnUIThread(new MainNavigationMessage(MainNavigationMessages.PROFILE));

        public void Logout()
        {
            _userService.User = null;
            _personUserService.PersonUser = null;
            MenuItems.Clear();
            _eventAggregator.PublishOnUIThread(new DataMessage(DataMessages.CLEAR_ALL));
            _eventAggregator.PublishOnUIThread(new NavigationMessage(NavigationMessages.AUTHENTICATION));
        }
    }
}