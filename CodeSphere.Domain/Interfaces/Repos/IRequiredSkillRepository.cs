using CodeSphere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Interfaces.Repos
{
    public interface IRequiredSkillRepository : IRepository<RequiredSkill>
    {
        Task<DataWithPagination<RequiredSkill>> GetSkillsForProjectAsync(int projectId, int pageNumber, int pageSize);
        Task AddSkillToProjectAsync(int projectId, RequiredSkill skill);
        Task DeleteSkillsForProjectAsync(int projectId);
        Task DeleteSkillForProjectAsync(int projectId, int skillId);
        Task UpdateSkillsForProjectAsync(int projectId, List<RequiredSkill> requiredSkills);
    }



}
