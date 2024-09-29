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
        Task<IEnumerable<Project>> GetProjectsByClientIdAsync(int clientId);
        Task<(List<Project>, PaginationMetaData)> Search(int pageNumber, int pageSize, List<string> filterWords);

    }

}
