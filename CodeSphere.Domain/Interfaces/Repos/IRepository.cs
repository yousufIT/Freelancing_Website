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
        Task<DataWithPagination<T>> GetAllAsync(int pageNumber, int pageSize);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }

    public class DataWithPagination<T>
    {
        public List<T> Items { get; set; }
        public PaginationMetaData PaginationMetaData { get; set; }
    }
}
