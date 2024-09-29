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
        Task<IEnumerable<Bid>> GetBidsByProjectIdAsync(int projectId);
        Task<IEnumerable<Bid>> GetBidsByFreelancerIdAsync(int freelancerId);
    }

}
