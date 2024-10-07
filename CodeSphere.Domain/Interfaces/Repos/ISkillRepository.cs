using CodeSphere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Interfaces.Repos
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task<List<Skill>> GetSkillsForFreelancerAsync(int freelancerId);
        Task AddSkillToFreelancerAsync(int freelancerId, Skill skill);
        Task UpdateSkillsForFreelancerAsync(int freelancerId, List<Skill> skills);
        Task DeleteSkillsForFreelancerAsync(int freelancerId);
    }

}
