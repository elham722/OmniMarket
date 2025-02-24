﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OmniMarket.Application.Persistence.Contracts;
using OmniMarket.Persistence.Context;

namespace OmniMarket.Persistence.Repositories
{
   public class GenericRepository<T> :IGenericRepository<T> where T : class
   {
       private readonly OmniMarketDbContext _context;

       public GenericRepository(OmniMarketDbContext context)
       {
           _context = context;
       }
       public async Task<T> GetByIdAsync(Guid id)
       {
           return await _context.Set<T>().FindAsync(id);
       }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State=EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
             _context.Set<T>().Remove(entity);
             await _context.SaveChangesAsync();
        }

        public async Task<bool> Exist(Guid id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null;
        }
    }
}
