using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using VehicleDiary.Models;

namespace VehicleDiary.Services
{
    public class VehicleService
    {
        private readonly VehicleDiaryDbContext _context;
        public VehicleModel Vehicle { get; set; }
        public VehicleModel VehicleForSaleListing { get; set; }
        public SaleListingModel SaleListingForEdit { get; set; }
        public VehicleServiceModel VehicleServiceForServiceView { get; set; }

        private static readonly int RECOMMENDED_SERVICE_COUNT_PER_YEAR = 4;
        private static readonly int MAXIMUM_ACCIDENT_COUNT = 4;
        public bool saleListingsLoaded = false;
        public List<SaleListingModel> SaleListings { get; set; }

        public VehicleService(VehicleDiaryDbContext vehicleDiaryDbContext)
        {
            _context = vehicleDiaryDbContext;
            SaleListings = new List<SaleListingModel>();
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

        public async Task DeleteSaleListing(SaleListingModel saleListing)
        {
            SaleListingModel res = await _context.Set<SaleListingModel>().FindAsync(saleListing.Id);
            _context.Set<SaleListingModel>().Remove(res);
            await _context.SaveChangesAsync();
        }

        // SALE LISTINGS
        public async Task<IEnumerable<SaleListingModel>> GetSaleListingsWithVehicles()
        {
            return await _context.Set<SaleListingModel>().Include(sl => sl.Vehicle).ToListAsync();
        }

        public async Task<double> GetSaleListingScore(int id)
        {
            SaleListingModel saleListing = await _context.Set<SaleListingModel>()
                .Where(s => s.Id == id)
                .Include(s => s.Vehicle)
                .FirstOrDefaultAsync();

            VehicleModel vehicle = await _context.Set<VehicleModel>()
                .Where(v => v.Vin == saleListing.Vehicle.Vin)
                .Include(v => v.VehicleSpecification)
                .Include(v => v.Services)
                .Include(v => v.Accidents)
                .FirstOrDefaultAsync();

            float averageAskingPriceForModel = _context.Set<SaleListingModel>().Where(s => s.Vehicle.Make == vehicle.Make).Where(s => s.Vehicle.Model == vehicle.Model).Average(s => s.Price);
            int numberOfServices = vehicle.Services.Count;
            int numberOfAccidents = vehicle.Accidents.Count;

            int numberOfAuthorizedServices = _context.Set<VehicleServiceModel>().Where(s => s.Vehicle.Vin == vehicle.Vin).Where(s => s.ServicedBy.ServiceType == Enums.UserEnums.ServiceType.AUTHORIZED).Count();

            DateTime today = DateTime.Now;

            double priceScore = (-((averageAskingPriceForModel - saleListing.Price) / averageAskingPriceForModel) + 0.5) * 30;

            int yearDifference = today.Year - vehicle.VehicleSpecification.MakeDate.Year;
            int servicesDifference = numberOfServices - numberOfAuthorizedServices;

            double serviceScore = (numberOfServices / (RECOMMENDED_SERVICE_COUNT_PER_YEAR * (yearDifference > 0 ? yearDifference : 1))) * 40 * (numberOfServices / (servicesDifference > 0 ? servicesDifference : 1));

            double accidentScore = (MAXIMUM_ACCIDENT_COUNT - numberOfAccidents) / MAXIMUM_ACCIDENT_COUNT;

            return priceScore + serviceScore + accidentScore;
        }

        public async Task<SaleListingModel> UpdateSaleListing(SaleListingModel saleListingModel)
        {
            _context.Set<SaleListingModel>().AddOrUpdate(saleListingModel);
            await _context.SaveChangesAsync();
            return saleListingModel;
        }

        // VEHICLE SERVICE

        public async Task<VehicleServiceModel> GetVehicleServiceData(int vehicleServiceId)
        {
            return await _context.Set<VehicleServiceModel>().Where(vs => vs.Id == vehicleServiceId).Include(vs => vs.ServicedBy).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<VehicleServiceModel>> GetVehicleServicesForServiceUser(int serviceUserId)
        {
            return await _context.Set<VehicleServiceModel>().Where(vs => vs.ServicedBy.Id == serviceUserId).Include(vs => vs.Vehicle).ToListAsync();
        }
    }
}
