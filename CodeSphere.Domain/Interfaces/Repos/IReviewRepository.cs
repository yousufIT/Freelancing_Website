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
        Task DeleteReviewsByClientIdAsync(int clientId);
        Task<DataWithPagination<Review>> GetReviewsByClientIdAsync(int clientId, int pageNumber, int pageSize);
        Task<DataWithPagination<Review>> GetReviewsByFreelancerIdAsync(int freelancerId, int pageNumber, int pageSize);
        Task DeleteReviewsByFreelancerIdAsync(int freelancerId);
    }


}
