using Caliburn.Micro;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using VehicleDiary.Models;
using VehicleDiary.Services;
using static VehicleDiary.Models.Enums.VehicleEnums;

namespace VehicleDiary.Main.ViewModels
{
    public class VehicleReportViewModel : Screen
    {
        private readonly VehicleService _vehicleService;
        private VehicleModel _vehicle;
        public BindableCollection<VehicleAccidentViewModel> Accidents { get; set; }
        public BindableCollection<VehicleServiceViewModel> Services { get; set; }

        public VehicleReportViewModel(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            // COMMENT: ensured that this screen will be activated AFTER loading a vehicle in VehicleService
            _vehicle = _vehicleService.Vehicle;
            Accidents = new BindableCollection<VehicleAccidentViewModel>();
            Accidents.AddRange(_vehicle.Accidents.Select(accident => new VehicleAccidentViewModel(accident)));

            Services = new BindableCollection<VehicleServiceViewModel>();
            Services.AddRange(_vehicle.Services.Select(service => new VehicleServiceViewModel(service)));
        }

        public string Vin => _vehicle.Vin;
        public Make Make => _vehicle.Make;
        public Model Model => _vehicle.Model;
        public DateTime MakeDate => _vehicle.VehicleSpecification.MakeDate;
        public BodyStyle BodyStyle => _vehicle.VehicleSpecification.BodyStyle;
        public DriveType DriveType => _vehicle.VehicleSpecification.DriveType;
        public float Kilometrage => _vehicle.VehicleSpecification.Kilometrage;
        public FuelType FuelType => _vehicle.VehicleSpecification.FuelType;
        public int EngineVolume => _vehicle.VehicleSpecification.EngineVolume;
        public int EnginePowerKW => _vehicle.VehicleSpecification.EnginePowerKW;
        public EngineEmissionType EngineEmissionType => _vehicle.VehicleSpecification.EngineEmissionType;
        public GearboxType GearboxType => _vehicle.VehicleSpecification.GearboxType;

        public RelayCommand<Visual> PrintToPdf
        {
            get
            {
                return new RelayCommand<Visual>(visual =>
                {
                    PrintDialog printDlg = new PrintDialog();
                    printDlg.PrintVisual(visual, "Vehicle report");
                });
            }
        }
    }
}
