using Caliburn.Micro;
using System;
using System.Threading.Tasks;
using System.Windows;
using VehicleDiary.Main.Messages;
using VehicleDiary.Models;
using VehicleDiary.Services;
using static VehicleDiary.Models.Enums.VehicleEnums;

namespace VehicleDiary.Main.ViewModels
{
    public class VehicleViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly VehicleModel _vehicle;
        private readonly VehicleService _vehicleService;
        private readonly UniversalCRUDService<VehicleModel> _vehicleCrudService;
        private readonly UniversalCRUDService<VehicleSpecificationModel> _vehicleSpecificationCrudService;
        private readonly UniversalCRUDService<SaleListingModel> _saleListingCrudService;

        public VehicleViewModel(
            IEventAggregator eventAggregator,
            VehicleModel vehicle,
            VehicleService vehicleService,
            UniversalCRUDService<VehicleModel> vehicleCrudService,
            UniversalCRUDService<VehicleSpecificationModel> vehicleSpecificationCrudService,
            UniversalCRUDService<SaleListingModel> saleListingCrudService)
        {
            _eventAggregator = eventAggregator;
            _vehicle = vehicle;
            _vehicleService = vehicleService;
            _vehicleCrudService = vehicleCrudService;
            _vehicleSpecificationCrudService = vehicleSpecificationCrudService;
            _saleListingCrudService = saleListingCrudService;
        }

        public string Vin => _vehicle.Vin;
        public Make Make => _vehicle.Make;
        public Model Model => _vehicle.Model;

        public event EventHandler VehicleRemoved;

        public string SaleListingActionLabel => _vehicle.SaleListing == null ? "List for sale" : "View sale listing";

        public void SetVehicleSaleListing(SaleListingModel saleListing)
        {
            _vehicle.SaleListing = saleListing;
            NotifyOfPropertyChange(SaleListingActionLabel);
            Refresh();
        }

        public void SaleListingAction()
        {
            if (_vehicle.SaleListing == null)
            {
                _vehicleService.VehicleForSaleListing = _vehicle;
                _eventAggregator.PublishOnUIThread(new MainNavigationMessage(MainNavigationMessages.VEHICLE_SALE_LISTING));
            }
            else
            {
                // TODO: 
            }
        }

        public async Task ViewReport()
        {
            try
            {
                VehicleModel vehicle = await _vehicleService.GetVehicleWithAllData(Vin);
                _vehicleService.Vehicle = vehicle;
                _eventAggregator.PublishOnUIThread(new MainNavigationMessage(MainNavigationMessages.VEHICLE_REPORT));
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured while acquiring the report for this vehicle", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task DeleteVehicle()
        {
            await _vehicleSpecificationCrudService.Delete(_vehicle.VehicleSpecification.Id);
            await _vehicleCrudService.Delete(_vehicle.Vin);
            VehicleRemoved?.Invoke(this, EventArgs.Empty);

        }

        public async Task Remove()
        {
            try
            {
                if (_vehicle.SaleListing != null)
                {
                    if (MessageBox.Show("A sale listing for this vehicle exists. By deleting the vehicle, its sale listing will be deleted as well.", "Sale listing exists", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    {
                        await _saleListingCrudService.Delete(_vehicle.SaleListing.Id);
                        await DeleteVehicle();
                    }
                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to delete this vehicle?", "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        await DeleteVehicle();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured while deleting this vehicle", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
