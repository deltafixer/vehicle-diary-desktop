using Caliburn.Micro;
using System;
using System.Linq;
using System.Collections.Generic;
using static VehicleDiary.Models.Enums.UserEnums;
using VehicleDiary.Authenticate.Messages;

namespace VehicleDiary.Authenticate.ViewModels
{
    public class RegisterServiceViewModel : Screen
    {
        private IEventAggregator _eventAggregator;
        private string _selectedServiceType = ServiceType.AUTHORIZED.ToString();

        public RegisterServiceViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public List<string> ServiceTypes
        {
            get => Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>().Select(v => v.ToString()).ToList();
        }

        public string SelectedServiceType { get => _selectedServiceType; set => Set(ref _selectedServiceType, value); }

        public void Back() => _eventAggregator.PublishOnUIThread(new AuthenticationNavigationMessage(AuthenticationNavigationOptions.RegisterType));
    }
}