using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VehicleDiary.Main.Messages;
using VehicleDiary.Models;
using VehicleDiary.Services;
using static VehicleDiary.Models.Enums.UserEnums;
using static VehicleDiary.Models.Enums.VehicleEnums;

namespace VehicleDiary.Main.ViewModels
{
    public class MyVehiclesViewModel : Screen, IHandle<DataMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private UniversalCRUDService<VehicleSpecificationModel> _vehicleSpecificationService;
        private UniversalCRUDService<PersonUserVehicleModel> _personUserVehicleService;
        private UniversalCRUDService<VehicleModel> _vehicleService;
        private readonly UserService _userService;
        private readonly PersonUserService _personUserService;
        private string _selectedMake = Make.AUDI.ToString();
        private string _selectedModel = Model.A1.ToString();
        private string _vin;
        private DateTime _makeDate = DateTime.Now;
        private string _selectedBodyStyle = BodyStyle.COUPE.ToString();
        private string _selectedDriveType = DriveType.FRONT.ToString();
        private string _kilometrage;
        private string _selectedFuelType = FuelType.DIESEL.ToString();
        private string _engineVolume;
        private string _enginePowerKW;
        private string _selectedEngineEmissionType = EngineEmissionType.EURO_4.ToString();
        private string _selectedGearboxType = GearboxType.AUTOMATIC.ToString();

        public string SelectedMake { get => _selectedMake; set => Set(ref _selectedMake, value); }
        public string SelectedModel { get => _selectedModel; set => Set(ref _selectedModel, value); }
        public string Vin { get => _vin; set { _vin = value; NotifyOfPropertyChange(() => Vin); NotifyOfPropertyChange(() => CanAddVehicle); } }
        public DateTime MakeDate { get => _makeDate; set => Set(ref _makeDate, value); }
        public string SelectedBodyStyle { get => _selectedBodyStyle; set => Set(ref _selectedBodyStyle, value); }
        public string SelectedDriveType { get => _selectedDriveType; set => Set(ref _selectedDriveType, value); }
        public string Kilometrage { get => _kilometrage; set { _kilometrage = value; NotifyOfPropertyChange(() => Kilometrage); NotifyOfPropertyChange(() => CanAddVehicle); } }
        public string SelectedFuelType { get => _selectedFuelType; set => Set(ref _selectedFuelType, value); }
        public string EngineVolume { get => _engineVolume; set { _engineVolume = value; NotifyOfPropertyChange(() => EngineVolume); NotifyOfPropertyChange(() => CanAddVehicle); } }
        public string EnginePowerKW { get => _enginePowerKW; set { _enginePowerKW = value; NotifyOfPropertyChange(() => EnginePowerKW); NotifyOfPropertyChange(() => CanAddVehicle); } }
        public string SelectedEngineEmissionType { get => _selectedEngineEmissionType; set => Set(ref _selectedEngineEmissionType, value); }
        public string SelectedGearboxType { get => _selectedGearboxType; set => Set(ref _selectedGearboxType, value); }
        public BindableCollection<VehicleModel> Vehicles { get; set; }

        public MyVehiclesViewModel(IEventAggregator eventAggregator, UniversalCRUDService<VehicleSpecificationModel> vehicleSpecificationService, UniversalCRUDService<PersonUserVehicleModel> personUserVehicleService, PersonUserService personUserService, UniversalCRUDService<VehicleModel> vehicleService, UserService userService)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _vehicleSpecificationService = vehicleSpecificationService;
            _personUserVehicleService = personUserVehicleService;
            _vehicleService = vehicleService;
            _personUserService = personUserService;
            _userService = userService;
            Vehicles = new BindableCollection<VehicleModel>();
        }

        public List<string> Makes
        {
            get => Enum.GetValues(typeof(Make)).Cast<Make>().Select(v => v.ToString()).ToList();
        }

        public List<string> Models
        {
            get => Enum.GetValues(typeof(Model)).Cast<Model>().Select(v => v.ToString()).ToList();
        }

        public List<string> BodyStyles
        {
            get => Enum.GetValues(typeof(BodyStyle)).Cast<BodyStyle>().Select(v => v.ToString()).ToList();
        }

        public List<string> DriveTypes
        {
            get => Enum.GetValues(typeof(DriveType)).Cast<DriveType>().Select(v => v.ToString()).ToList();
        }

        public List<string> FuelTypes
        {
            get => Enum.GetValues(typeof(FuelType)).Cast<FuelType>().Select(v => v.ToString()).ToList();
        }

        public List<string> EngineEmissionTypes
        {
            get => Enum.GetValues(typeof(EngineEmissionType)).Cast<EngineEmissionType>().Select(v => v.ToString()).ToList();
        }

        public List<string> GearboxTypes
        {
            get => Enum.GetValues(typeof(GearboxType)).Cast<GearboxType>().Select(v => v.ToString()).ToList();
        }


        public async void AddVehicle()
        {
            try
            {
                VehicleModel vehicle = new VehicleModel { Make = (Make)Enum.Parse(typeof(Make), SelectedMake), Model = (Model)Enum.Parse(typeof(Model), SelectedModel), Vin = Vin };
                await _vehicleSpecificationService.Create(new VehicleSpecificationModel { MakeDate = MakeDate, BodyStyle = (BodyStyle)Enum.Parse(typeof(BodyStyle), SelectedBodyStyle), DriveType = (DriveType)Enum.Parse(typeof(DriveType), SelectedDriveType), Kilometrage = float.Parse(Kilometrage), FuelType = (FuelType)Enum.Parse(typeof(FuelType), SelectedFuelType), EngineVolume = int.Parse(EngineVolume), EnginePowerKW = int.Parse(EnginePowerKW), EngineEmissionType = (EngineEmissionType)Enum.Parse(typeof(EngineEmissionType), SelectedEngineEmissionType), GearboxType = (GearboxType)Enum.Parse(typeof(GearboxType), SelectedGearboxType), Vehicle = vehicle });

                await _personUserVehicleService.Create(new PersonUserVehicleModel { Vin = vehicle.Vin, Id = _personUserService.PersonUser.Id });

                if (MessageBox.Show("Successfully added a vehicle with VIN: " + $"{Vin}", "Success", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    ClearFields();
                }
            }
            catch (Exception)
            {
                if (MessageBox.Show("Vehicle with this VIN already exists in the database", "Error", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    Vin = string.Empty;
                }
            }
        }

        public bool CanAddVehicle => (Vin?.Length == 17 && Kilometrage?.Length > 0 && EngineVolume?.Length > 0 && EnginePowerKW?.Length > 0);

        public void ClearFields()
        {
            Vin = Kilometrage = EngineVolume = EnginePowerKW = string.Empty;
            SelectedMake = Make.AUDI.ToString();
            SelectedModel = Model.A1.ToString();
            MakeDate = DateTime.Now;
            SelectedBodyStyle = BodyStyle.COUPE.ToString();
            SelectedDriveType = DriveType.FRONT.ToString();
            SelectedFuelType = FuelType.DIESEL.ToString();
            SelectedEngineEmissionType = EngineEmissionType.EURO_4.ToString();
            SelectedGearboxType = GearboxType.AUTOMATIC.ToString();
        }

        public async void Handle(DataMessage dataMessage)
        {
            switch (dataMessage.Message)
            {
                case DataMessages.USER:
                    switch (_userService.User.UserType)
                    {
                        case UserType.PERSON:
                            IEnumerable<PersonUserVehicleModel> personUserVehicleReferences = await _personUserService.GetPersonUserVehicleVins(_userService.User.Username);
                            List<string> vins = new List<string>();
                            foreach (PersonUserVehicleModel personUserVehicleReference in personUserVehicleReferences)
                            {
                                vins.Add(personUserVehicleReference.Vin);
                            }
                            IEnumerable<VehicleModel> personUserVehicles  = await _personUserService.GetPersonUserVehicles(vins);
                            foreach(VehicleModel vehicle in personUserVehicles)
                            {
                                _personUserService.Vehicles.Add(vehicle);
                            }
                            Vehicles = new BindableCollection<VehicleModel>(_personUserService.Vehicles);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
