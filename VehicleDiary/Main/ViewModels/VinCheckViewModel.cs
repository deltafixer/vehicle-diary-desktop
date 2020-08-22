using Caliburn.Micro;
using VehicleDiary.Models;
using VehicleDiary.Services;

namespace VehicleDiary.Main.ViewModels
{
    public class VinCheckViewModel : Screen
    {
        private readonly UniversalCRUDService<VehicleModel> _vehicleCRUDService;
        private string _vin;

        public VinCheckViewModel(UniversalCRUDService<VehicleModel> vehicleCRUDService)
        {
            _vehicleCRUDService = vehicleCRUDService;
        }

        public string Vin
        {
            get => _vin; set { _vin = value; NotifyOfPropertyChange(() => Vin); NotifyOfPropertyChange(() => CanGetVinReport); }
        }

        public bool CanGetVinReport => Vin?.Length == 17;

        public async void GetVinReport()
        {
            VehicleModel vehicle = await _vehicleCRUDService.Get(Vin);
        }
    }
}
