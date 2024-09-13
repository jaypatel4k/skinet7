using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    //public class GenericRepository<T>(StoreContext context) : IGenericRepository<T> where T : BaseEntity
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;
        public GenericRepository(StoreContext context)
        {
            _context = context;
        }
        public void Add(T Entity)
        {
            _context.Set<T>().Add(Entity);
        }
        public void Remove(T Entity)
        {
            _context.Set<T>().Remove(Entity);
        }
        public bool Exists(int id)
        {
            return _context.Set<T>().Any(x=> x.Id == id);
        }
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x=>x.Id == id);
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public void Update(T Entity)
        {
            _context.Set<T>().Attach(Entity);
            _context.Entry(Entity).State = EntityState.Modified;
        }
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(),spec);
        }
        //Below 3 method is added for 39. Adding projection to Spec Part 2
        public async Task<TResult> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T,TResult> spec)
        {
            return SpecificationEvaluator<T>.GetQuery<T,TResult>(_context.Set<T>().AsQueryable(),spec);
        }
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            var query = _context.Set<T>().AsQueryable();
            query = spec.ApplyCriteria(query);
            return await query.CountAsync();
        }
    }
}