using CodeSphere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Interfaces.Repos
{
    public interface IBidRepository : IRepository<Bid>
    {
        Task<DataWithPagination<Bid>> GetBidsByProjectIdAsync(int projectId, int pageNumber, int pageSize);
        Task<DataWithPagination<Bid>> GetBidsByFreelancerIdAsync(int freelancerId, int pageNumber, int pageSize);
        Task DeleteBidsForProjectAsync(int projectId); 
    }

}
