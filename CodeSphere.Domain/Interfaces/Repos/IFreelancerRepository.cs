using CodeSphere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Interfaces.Repos
{
    public interface IFreelancerRepository : IRepository<Freelancer>
    {
        Task<DataWithPagination<Freelancer>> GetFreelancersBySkillAsync(string skill, int pageNumber, int pageSize);
        Task<List<Freelancer>> GetTopFreelancersByRatingAsync(int count);

    }


}
