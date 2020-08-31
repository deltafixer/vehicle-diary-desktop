using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VehicleDiary.Models;

namespace VehicleDiary.Services
{
    public class VehicleService
    {
        private readonly VehicleDiaryDbContext _context;
        public VehicleModel Vehicle { get; set; }
        public VehicleModel VehicleForNewSaleListing { get; set; }

        public VehicleService(VehicleDiaryDbContext vehicleDiaryDbContext)
        {
            _context = vehicleDiaryDbContext;
        }

        public async Task<VehicleModel> GetVehicleWithAllData(string vin)
        {
            return await _context.Set<VehicleModel>().Where(v => v.Vin == vin).Include(v => v.VehicleSpecification).Include(v => v.Accidents).Include(v => v.Services).Include(v => v.SaleListing).FirstOrDefaultAsync();
        }

        public async Task<VehicleModel> GetVehicle(string vin)
        {
            return await _context.Set<VehicleModel>().Where(v => v.Vin == vin).FirstOrDefaultAsync();
        }

        // COMMENT: in order to match existing objects as foreign keys for other objects, using the same context is required, thus, functions are located here
        public async Task<VehicleAccidentModel> CreateVehicleAccidentReport(VehicleAccidentModel accident)
        {
            var res = _context.Set<VehicleAccidentModel>().Add(accident);
            await _context.SaveChangesAsync();
            return res;
        }

        public async Task<VehicleServiceModel> CreateVehicleService(VehicleServiceModel service)
        {
            var res = _context.Set<VehicleServiceModel>().Add(service);
            await _context.SaveChangesAsync();
            return res;
        }

        public async Task<SaleListingModel> CreateSaleListing(SaleListingModel saleListing)
        {
            var res = _context.Set<SaleListingModel>().Add(saleListing);
            await _context.SaveChangesAsync();
            return res;
        }

        public async Task<ServiceUserModel> GetServiceUser(int id)
        {
            return await _context.Set<ServiceUserModel>().FindAsync(id);
        }
    }
}
