﻿using Caliburn.Micro;
using VehicleDiary.Main.Messages;
using VehicleDiary.Models;
using VehicleDiary.Services;
using static VehicleDiary.Models.Enums.UserEnums;

namespace VehicleDiary.Main.ViewModels
{
    public class CustomMenuItem
    {
        public string Name { get; set; }
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
                                new CustomMenuItem { Name = "Check VIN" },
                                new CustomMenuItem { Name = "My vehicles" },
                                new CustomMenuItem { Name = "Market" },
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
                    break;
                default:
                    break;
            }
        }

        public void Logout()
        {
            _userService.User = null;
            _personUserService.PersonUser = null;
            MenuItems.Clear();
            _eventAggregator.PublishOnUIThread(new DataMessage(DataMessages.CLEAR_ALL));
            _eventAggregator.PublishOnUIThread(new NavigationMessage(NavigationMessages.AUTHENTICATION));
        }

        public void Profile() => _eventAggregator.PublishOnUIThread(new MainNavigationMessage(MainNavigationMessages.PROFILE));
    }
}