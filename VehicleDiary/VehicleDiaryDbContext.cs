using System.Data.Entity;
using VehicleDiary.Models;

namespace VehicleDiary
{
    class VehicleDiaryDbContext : DbContext
    {
        public DbSet<PersonUserModel> PersonUsers { get; set; }
        public DbSet<VehicleModel> Vehicle { get; set; }
        public DbSet<VehicleSpecificationModel> VehicleSpecifications { get; set; }
    }
}
