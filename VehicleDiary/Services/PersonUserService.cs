using Caliburn.Micro;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VehicleDiary.Main.Messages;
using VehicleDiary.Models;

namespace VehicleDiary.Services
{
    public class PersonUserService : IHandle<DataMessage>
    {
        private readonly VehicleDiaryDbContext _context;
        private readonly IEventAggregator _eventAggregator;
        public PersonUserModel PersonUser { get; set; }
        public List<VehicleModel> Vehicles { get; set; }

        public PersonUserService(VehicleDiaryDbContext vehicleDiaryDbContext, IEventAggregator eventAggregator)
        {
            _context = vehicleDiaryDbContext;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            Vehicles = new List<VehicleModel>();
        }

        public async Task<PersonUserModel> Get(string username)
        {
            return await _context.Set<PersonUserModel>().Where(o => o.User.Username == username).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PersonUserVehicleModel>> GetPersonUserVehicleVins(string username)
        {
            return await _context.Set<PersonUserVehicleModel>().Where(o => o.PersonUser.User.Username == username).ToListAsync();
        }

        // COMMENT: in ideal case, this, and the function above would be in one
        // I couldn't figure out in the moment of writing whether this is possible in my database design
        public async Task<IEnumerable<VehicleModel>> GetPersonUserVehicles(List<string> vins)
        {
            return await _context.Set<VehicleModel>().Where(o => vins.Contains(o.Vin)).Include(v => v.VehicleSpecification).Include(v => v.SaleListing).ToListAsync();
        }

        public void Handle(DataMessage message)
        {
            if (message.Message == DataMessages.CLEAR_ALL)
            {
                Vehicles.Clear();
            }
        }
    }
}
