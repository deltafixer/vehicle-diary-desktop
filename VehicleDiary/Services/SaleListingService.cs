using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using VehicleDiary.Models;

namespace VehicleDiary.Services
{
    public class SaleListingService
    {
        private readonly VehicleDiaryDbContext _context;

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
    }
}
