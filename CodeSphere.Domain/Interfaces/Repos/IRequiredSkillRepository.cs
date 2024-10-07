using CodeSphere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Interfaces.Repos
{
    public interface IRequiredSkillRepository : IRepository<Skill>
    {
        Task<List<Skill>> GetSkillsForProjectAsync(int projectId);
        Task AddSkillToProjectAsync(int projectId, Skill skill);
        Task DeleteSkillsForProjectAsync(int projectId);
        Task DeleteSkillForProjectAsync(int projectId, int skillId);
    }



}
