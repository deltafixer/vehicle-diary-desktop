using Caliburn.Micro;
using System;
using System.Windows;
using VehicleDiary.Models;
using VehicleDiary.Services;

namespace VehicleDiary.Main.ViewModels
{
    public class ReportAccidentViewModel : Screen
    {
        private readonly VehicleService _vehicleService;
        private string _vin;
        private DateTime _dateOfAccident = DateTime.Now;
        private string _damagePriceEvaluation;

        public ReportAccidentViewModel(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public string Vin
        {
            get => _vin; set { _vin = value; NotifyOfPropertyChange(() => Vin); NotifyOfPropertyChange(() => CanSubmitReport); }
        }

        public DateTime DateOfAccident
        {
            get => _dateOfAccident; set { _dateOfAccident = value; NotifyOfPropertyChange(() => DateOfAccident); NotifyOfPropertyChange(() => CanSubmitReport); }
        }

        public string DamagePriceEvaluation
        {
            get => _damagePriceEvaluation; set { _damagePriceEvaluation = value; NotifyOfPropertyChange(() => DamagePriceEvaluation); NotifyOfPropertyChange(() => CanSubmitReport); }
        }

        public bool CanSubmitReport => (Vin?.Length == 17 && DamagePriceEvaluation?.Length > 0);

        public async void SubmitReport()
        {
            try
            {
                VehicleModel vehicle = await _vehicleService.GetVehicle(Vin);
                if (vehicle != null)
                {
                    await _vehicleService.CreateVehicleAccidentReport(new VehicleAccidentModel {Vehicle = vehicle, DateOfAccident = DateOfAccident, DamagePriceEvaluation = float.Parse(DamagePriceEvaluation) });
                    MessageBox.Show("Successfully created an accident report!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Vehicle with this VIN does not exist in the database.", "No Vehicle", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Vin = string.Empty;
                }
            } catch (Exception)
            {
                MessageBox.Show("An error occured while creating a report for this vehicle", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            Vin = DamagePriceEvaluation = string.Empty;
        }
    }
}
