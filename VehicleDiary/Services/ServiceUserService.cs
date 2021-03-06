﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VehicleDiary.Models;

namespace VehicleDiary.Services
{
    public class ServiceUserService
    {
        private readonly VehicleDiaryDbContext _context;
        public ServiceUserModel ServiceUser { get; set; }
        public List<VehicleServiceModel> VehicleServices { get; set; }

        public ServiceUserService(VehicleDiaryDbContext vehicleDiaryDbContext)
        {
            _context = vehicleDiaryDbContext;
            VehicleServices = new List<VehicleServiceModel>();
        }

        public async Task<ServiceUserModel> Get(string username)
        {
            return await _context.Set<ServiceUserModel>().Where(o => o.User.Username == username).FirstOrDefaultAsync();
        }
    }
}
