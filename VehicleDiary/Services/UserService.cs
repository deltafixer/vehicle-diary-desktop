using System.Threading.Tasks;
using VehicleDiary.Models;

namespace VehicleDiary.Services
{
    public class UserService
    {
        private readonly VehicleDiaryDbContext _context;

        public UserService(VehicleDiaryDbContext vehicleDiaryDbContext)
        {
            _context = vehicleDiaryDbContext;
        }

        public UserModel User { get; set; }

        public async Task<UserModel> CheckCredentials(string username)
        {
            return await _context.Set<UserModel>().FindAsync(username);
        }
    }
}
