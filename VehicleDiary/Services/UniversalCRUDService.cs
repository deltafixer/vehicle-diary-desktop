using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using VehicleDiary.Models;

namespace VehicleDiary.Services
{
    public class UniversalCRUDService<T> : ICRUDService<T> where T : class
    {
        private readonly VehicleDiaryDbContext _context;

        public UniversalCRUDService(VehicleDiaryDbContext vehicleDiaryDbContext)
        {
            _context = vehicleDiaryDbContext;
        }

        public async Task<T> Create(T entity)
        {
            var res = _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return res;
        }

        public async Task<bool> Delete<S>(S id)
        {
            // overkill reflection attempt
            // var idType = id.GetType();
            // var idProperty = idType.GetProperty(propertyName);
            T res = await _context.Set<T>().FindAsync(id);

            _context.Set<T>().Remove(res);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<T> Get<S>(S id)
        {
            return _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> Update(T entity)
        {
            _context.Set<T>().AddOrUpdate(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }

    public class PersonUserVehicleService
    {
        private readonly VehicleDiaryDbContext _context;

        public PersonUserVehicleService(VehicleDiaryDbContext vehicleDiaryDbContext)
        {
            _context = vehicleDiaryDbContext;
        }

        public async Task<IEnumerable<PersonUserVehicleModel>> GetPersonUserVehicles(string username)
        {
            return await _context.Set<PersonUserVehicleModel>().Where(o => o.Username == username).ToListAsync();
        }
    }

}
