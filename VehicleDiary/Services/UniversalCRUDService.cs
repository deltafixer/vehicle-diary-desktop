﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;

namespace VehicleDiary.Services
{
    public class UniversalCRUDService<T> : ICRUDService<T> where T : class
    {
        // dependency injection issue: couldn't automagically use DbContext instance, manually creating on every request
        // autofac issue probably...
        private VehicleDiaryDbContext _context;

        public async Task<T> Create(T entity)
        {
            _context = new VehicleDiaryDbContext();
            var res = _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return res;
        }

        public async Task<bool> Delete<S>(S id)
        {
            // overkill reflection attempt
            // var idType = id.GetType();
            // var idProperty = idType.GetProperty(propertyName);
            _context = new VehicleDiaryDbContext();
            T res = await _context.Set<T>().FindAsync(id);

            _context.Set<T>().Remove(res);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<T> Get<S>(S id)
        {
            _context = new VehicleDiaryDbContext();
            return _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            _context = new VehicleDiaryDbContext();
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> Update(T entity)
        {
            _context = new VehicleDiaryDbContext();
            _context.Set<T>().AddOrUpdate(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }



}
