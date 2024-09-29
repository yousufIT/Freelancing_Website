using CodeSphere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Interfaces.Repos
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IEnumerable<Review>> GetReviewsByFreelancerIdAsync(int freelancerId);
        Task<IEnumerable<Review>> GetReviewsByClientIdAsync(int clientId);
        Task DeleteReviewsByFreelancerIdAsync(int freelancerId);
    }

}
