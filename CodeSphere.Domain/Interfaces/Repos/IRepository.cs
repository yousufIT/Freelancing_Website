using CodeSphere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Interfaces.Repos
{
    public interface IRepository<T> where T : IBase
    {
        Task<(List<T>, PaginationMetaData)> GetAllAsync(int pageNumber, int pageSize);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
