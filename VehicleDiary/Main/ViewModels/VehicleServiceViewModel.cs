using Caliburn.Micro;
using System;
using VehicleDiary.Main.Messages;
using VehicleDiary.Models;
using VehicleDiary.Services;

namespace VehicleDiary.Main.ViewModels
{
    public class VehicleServiceViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly VehicleServiceModel _vehicleServiceData;
        private readonly VehicleService _vehicleService;

        public VehicleServiceViewModel(
            IEventAggregator eventAggregator,
            VehicleServiceModel vehicleServiceModel,
            VehicleService vehicleService
            )
        {
            _eventAggregator = eventAggregator;
            _vehicleServiceData = vehicleServiceModel;
            _vehicleService = vehicleService;
        }

        public string DateTaken => _vehicleServiceData.DateTaken.ToShortDateString();
        public float Price => _vehicleServiceData.Price;
        public string ServiceDetails => _vehicleServiceData.ServiceDetails;

        public async void ServiceInfo()
        {
            _vehicleService.VehicleServiceForServiceView = await _vehicleService.GetVehicleServiceData(_vehicleServiceData.Id);
            _eventAggregator.PublishOnUIThread(new MainNavigationMessage(MainNavigationMessages.SERVICE_DETAILS));
        }
    }
}
