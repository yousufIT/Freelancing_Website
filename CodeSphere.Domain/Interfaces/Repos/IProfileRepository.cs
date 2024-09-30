using CodeSphere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Interfaces.Repos
{
    public interface IProfileRepository : IRepository<Profile>
    {
        Task DeleteByFreelancerIdAsync(int freelancerId);
        Task<Profile> GetProfileByFreelancerIdAsync(int freelancerId);
        Task<Profile> GetProfileWithPortfolioAsync(int profileId);
    }

}
