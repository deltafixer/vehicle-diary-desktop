using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VehicleDiary.Models;

namespace VehicleDiary.Services
{
    public class SaleListingService
    {
        private readonly VehicleDiaryDbContext _context;
        private static readonly int RECOMMENDED_SERVICE_COUNT_PER_YEAR = 4;
        private static readonly int MAXIMUM_ACCIDENT_COUNT = 4;

        public SaleListingService(VehicleDiaryDbContext vehicleDiaryDbContext)
        {
            _context = vehicleDiaryDbContext;
            SaleListings = new List<SaleListingModel>();
        }

        public List<SaleListingModel> SaleListings { get; set; }
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
    }
}
