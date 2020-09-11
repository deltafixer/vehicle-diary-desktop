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
        private readonly ServiceUserService _serviceUserService;
        public BindableCollection<CustomMenuItem> MenuItems { get; set; }

        public HeaderViewModel(
            IEventAggregator eventAggregator,
            UserService userService,
            PersonUserService personUserService,
            ServiceUserService serviceUserService)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _userService = userService;
            _personUserService = personUserService;
            _serviceUserService = serviceUserService;
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
                                new CustomMenuItem { Name = "My vehicles", OnClick = GoToMyVehicles},
                                new CustomMenuItem { Name = "Market", OnClick = GoToMarket },
                            });
                            PersonUserModel personUser = await _personUserService.Get(_userService.User.Username);
                            _personUserService.PersonUser = personUser;
                            _personUserService.PersonUser.User = _userService.User;
                            if (personUser.PersonType == PersonType.POLICE)
                            {
                                MenuItems.Insert(2, new CustomMenuItem { Name = "Report accident", OnClick = GoToReportAccident });
                            }
                            break;
                        case UserType.SERVICE:
                            MenuItems.AddRange(new BindableCollection<CustomMenuItem>
                            {
                                new CustomMenuItem { Name = "Add service", OnClick = GoToAddService },
                                new CustomMenuItem { Name = "My services", OnClick = GoToMyServices},
                            });
                            ServiceUserModel serviceUser = await _serviceUserService.Get(_userService.User.Username);
                            _serviceUserService.ServiceUser = serviceUser;
                            _serviceUserService.ServiceUser.User = _userService.User;
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
        public ICommand GoToReportAccident
        {
            get
            {
                return new CommandHandler(() => ActionGoToReportAccident());
            }
        }
        public ICommand GoToAddService
        {
            get
            {
                return new CommandHandler(() => ActionGoToAddService());
            }
        }
        public ICommand GoToMyServices
        {
            get
            {
                return new CommandHandler(() => ActionGoToMyServices());
            }
        }

        public void ActionGoToMyVehicles()
        {
            _eventAggregator.PublishOnUIThread(new MainNavigationMessage(MainNavigationMessages.MY_VEHICLES));
        }
        public void ActionGoToMarket()
        {
            _eventAggregator.PublishOnUIThread(new MainNavigationMessage(MainNavigationMessages.MARKET));
        }

        public void ActionGoToReportAccident()
        {
            _eventAggregator.PublishOnUIThread(new MainNavigationMessage(MainNavigationMessages.REPORT_ACCIDENT));
        }
        public void ActionGoToAddService()
        {
            _eventAggregator.PublishOnUIThread(new MainNavigationMessage(MainNavigationMessages.ADD_SERVICE));
        }
        public void ActionGoToMyServices()
        {
            _eventAggregator.PublishOnUIThread(new MainNavigationMessage(MainNavigationMessages.MY_SERVICES));
        }

        public void GoToCheckVin() => _eventAggregator.PublishOnUIThread(new MainNavigationMessage(MainNavigationMessages.CHECK_VIN));
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