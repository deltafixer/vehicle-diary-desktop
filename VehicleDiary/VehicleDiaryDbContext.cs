using System.Data.Entity;
using VehicleDiary.Models;

namespace VehicleDiary
{
    public class VehicleDiaryDbContext : DbContext
    {
        public DbSet<VehicleModel> Vehicles { get; set; }
        public DbSet<PersonUserModel> PersonUsers { get; set; }
        public DbSet<ServiceUserModel> ServiceUsers { get; set; }
        public DbSet<SaleListingModel> SaleListings { get; set; }
        public DbSet<VehicleAccidentModel> VehicleAccidents { get; set; }
        public DbSet<VehicleServiceModel> VehicleServices { get; set; }
        public DbSet<VehicleSpecificationModel> VehicleSpecifications { get; set; }
        public DbSet<PersonUserVehicleModel> PersonUsersVehicles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonUserVehicleModel>().HasKey(obj => new { obj.Username, obj.Vin});
        }
    }
}
