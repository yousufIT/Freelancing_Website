using CodeSphere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Interfaces.Repos
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<DataWithPagination<Project>> GetProjectsByClientIdAsync(int clientId, int pageNumber, int pageSize);
        Task DeleteProjectsByClientIdAsync(int clientId); 
    }


}
