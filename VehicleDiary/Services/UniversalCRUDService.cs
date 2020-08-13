using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(int id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
