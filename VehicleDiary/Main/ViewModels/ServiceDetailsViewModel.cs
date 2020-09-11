using Caliburn.Micro;
using VehicleDiary.Services;
using static VehicleDiary.Models.Enums.UserEnums;
using static VehicleDiary.Models.Enums.VehicleEnums;

namespace VehicleDiary.Main.ViewModels
{
    public class ServiceDetailsViewModel : Screen
    {
        private readonly VehicleService _vehicleService;
        public ServiceDetailsViewModel(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public Make Make => _vehicleService.VehicleServiceForServiceView.Vehicle.Make;
        public Model Model => _vehicleService.VehicleServiceForServiceView.Vehicle.Model;
        public string Vin => _vehicleService.VehicleServiceForServiceView.Vehicle.Vin;
        public string ServicedBy => _vehicleService.VehicleServiceForServiceView.ServicedBy.Name;
        public ServiceType ServiceType => _vehicleService.VehicleServiceForServiceView.ServicedBy.ServiceType;
        public float Price => _vehicleService.VehicleServiceForServiceView.Price;
        public string ServiceDetails => _vehicleService.VehicleServiceForServiceView.ServiceDetails;
    }
}
