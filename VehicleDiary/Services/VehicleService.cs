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

        public VehicleService(VehicleDiaryDbContext vehicleDiaryDbContext)
        {
            _context = vehicleDiaryDbContext;
        }

        public async Task<VehicleModel> GetVehicleWithAllData(string Vin)
        {
            return await _context.Set<VehicleModel>().Where(v => v.Vin == Vin).Include(v => v.VehicleSpecification).Include(v => v.Accidents).Include(v => v.Services).Include(v => v.SaleListing).FirstOrDefaultAsync();
        }
    }
}
