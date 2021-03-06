﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using VehicleDiary.Models;
using VehicleDiary.Services;

namespace VehicleDiary.Main.ViewModels
{
    public class MyServicesViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ServiceUserService _serviceUserService;
        private readonly VehicleService _vehicleService;
        private float _totalServicesPrice = 0;
        public BindableCollection<VehicleServiceViewModel> VehicleServices { get; set; }
        public float TotalServicesPrice { get => _totalServicesPrice; set { _totalServicesPrice = value; NotifyOfPropertyChange(() => TotalServicesPrice); } }

        public MyServicesViewModel(
            IEventAggregator eventAggregator,
            ServiceUserService serviceUserService,
            VehicleService vehicleService)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _serviceUserService = serviceUserService;
            _vehicleService = vehicleService;
            VehicleServices = new BindableCollection<VehicleServiceViewModel>();
        }

        protected override async void OnActivate()
        {
            base.OnActivate();
            if (_serviceUserService.ServiceUser != null)
            {
                IEnumerable<VehicleServiceModel> vehicleServices = await _vehicleService.GetVehicleServicesForServiceUser(_serviceUserService.ServiceUser.Id);
                foreach (VehicleServiceModel vehicleService in vehicleServices)
                {
                    _serviceUserService.VehicleServices.Add(vehicleService);
                    TotalServicesPrice += vehicleService.Price;
                }
                VehicleServices.AddRange(_serviceUserService.VehicleServices.Select(vehicleService => new VehicleServiceViewModel(_eventAggregator, vehicleService, _vehicleService)));
                AllTimeSelectedColor = _selectedColor;
            }
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _serviceUserService.VehicleServices.Clear();
            VehicleServices.Clear();
            ThisMonthSelectedColor = _defaultColor;
            LastWeekSelectedColor = _defaultColor;
            TodaySelectedColor = _defaultColor;
        }

        // COMMENT: not the cleanest solution...
        private readonly string _selectedColor = "#0068b3";
        private readonly string _defaultColor = "#0095FF";
        private string _allTimeSelectedColor = "#0095FF";
        private string _thisMonthSelectedColor = "#0095FF";
        private string _lastWeekSelectedColor = "#0095FF";
        private string _todaySelectedColor = "#0095FF";
        public string AllTimeSelectedColor { get => _allTimeSelectedColor; set { _allTimeSelectedColor = value; NotifyOfPropertyChange(() => AllTimeSelectedColor); } }
        public string ThisMonthSelectedColor { get => _thisMonthSelectedColor; set { _thisMonthSelectedColor = value; NotifyOfPropertyChange(() => ThisMonthSelectedColor); } }
        public string LastWeekSelectedColor { get => _lastWeekSelectedColor; set { _lastWeekSelectedColor = value; NotifyOfPropertyChange(() => LastWeekSelectedColor); } }
        public string TodaySelectedColor { get => _todaySelectedColor; set { _todaySelectedColor = value; NotifyOfPropertyChange(() => TodaySelectedColor); } }

        public void AllTime()
        {
            VehicleServices.Clear();
            TotalServicesPrice = 0;
            VehicleServices.AddRange(_serviceUserService.VehicleServices.Select(vehicleService => { TotalServicesPrice += vehicleService.Price; return new VehicleServiceViewModel(_eventAggregator, vehicleService, _vehicleService); }));
            AllTimeSelectedColor = _selectedColor;
            ThisMonthSelectedColor = _defaultColor;
            LastWeekSelectedColor = _defaultColor;
            TodaySelectedColor = _defaultColor;
        }

        public void ThisMonth()
        {
            int currentMonth = DateTime.Today.Month;
            VehicleServices.Clear();
            TotalServicesPrice = 0;
            VehicleServices.AddRange(_serviceUserService.VehicleServices.FindAll(vs => vs.DateTaken.Month == currentMonth).Select(vehicleService => { TotalServicesPrice += vehicleService.Price; return new VehicleServiceViewModel(_eventAggregator, vehicleService, _vehicleService); }));
            AllTimeSelectedColor = _defaultColor;
            ThisMonthSelectedColor = _selectedColor;
            LastWeekSelectedColor = _defaultColor;
            TodaySelectedColor = _defaultColor;
        }

        public void LastWeek()
        {
            DateTime today = DateTime.Today;
            VehicleServices.Clear();
            DateTime mondayOfLastWeek = today.AddDays(-6);
            TotalServicesPrice = 0;
            VehicleServices.AddRange(_serviceUserService.VehicleServices.FindAll(vs => vs.DateTaken >= mondayOfLastWeek).Select(vehicleService => { TotalServicesPrice += vehicleService.Price; return new VehicleServiceViewModel(_eventAggregator, vehicleService, _vehicleService); }));
            AllTimeSelectedColor = _defaultColor;
            ThisMonthSelectedColor = _defaultColor;
            LastWeekSelectedColor = _selectedColor;
            TodaySelectedColor = _defaultColor;
        }

        public void Today()
        {
            DateTime today = DateTime.Today;
            VehicleServices.Clear();
            TotalServicesPrice = 0;
            VehicleServices.AddRange(_serviceUserService.VehicleServices.FindAll(vs => vs.DateTaken == today).Select(vehicleService => { TotalServicesPrice += vehicleService.Price; return new VehicleServiceViewModel(_eventAggregator, vehicleService, _vehicleService); }));
            AllTimeSelectedColor = _defaultColor;
            ThisMonthSelectedColor = _defaultColor;
            LastWeekSelectedColor = _defaultColor;
            TodaySelectedColor = _selectedColor;
        }
    }
}
