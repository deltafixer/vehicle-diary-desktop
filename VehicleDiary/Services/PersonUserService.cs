using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VehicleDiary.Models;

namespace VehicleDiary.Services
{
    public class PersonUserService
    {
        private readonly VehicleDiaryDbContext _context;
        public PersonUserModel PersonUser { get; set; }

        public PersonUserService(VehicleDiaryDbContext vehicleDiaryDbContext)
        {
            _context = vehicleDiaryDbContext;
        }

        public async Task<PersonUserModel> Get(string username)
        {
            return await _context.Set<PersonUserModel>().Where(o => o.User.Username == username).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PersonUserVehicleModel>> GetPersonUserVehicles(string username)
        {
            return await _context.Set<PersonUserVehicleModel>().Where(o => o.PersonUser.User.Username == username).ToListAsync();
        }
    }
}
