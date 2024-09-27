using CodeSphere.Domain.Interfaces;
using CodeSphere.Domain.Models;
using CodeSphere.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Infrastructure.Repos
{
    public class Repository<T> : IRepository<T> where T : class,IBase
    {
        protected readonly CodeSphereContext _context;
        protected readonly ILogger<Repository<T>> _logger;
        public Repository(CodeSphereContext context, ILogger<Repository<T>>
        logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<(List<T>, PaginationMetaData)> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalItemCount = await _context.Set<T>().CountAsync(e => !e.IsDeleted);
            var paginationData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var response = await _context.Set<T>()
                                .Where(e => !e.IsDeleted)
                                .Skip(pageSize * (pageNumber - 1))
                                .Take(pageSize)
                                .ToListAsync();

            return (response, paginationData);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
