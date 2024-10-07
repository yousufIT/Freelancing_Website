using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using CodeSphere.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Infrastructure.Repos
{
    public class RequiredSkillRepository : Repository<Skill>, IRequiredSkillRepository
    {
        public RequiredSkillRepository(CodeSphereContext context, ILogger<Repository<Skill>> logger)
            : base(context, logger) { }

  
        public async Task AddSkillToProjectAsync(int projectId, Skill skill)
        {
            var project = await _context.Projects
                .Include(p => p.RequiredSkills)
                .FirstOrDefaultAsync(p => p.Id == projectId);
            
            project.RequiredSkills.Add(skill);
            await AddAsync(skill);
        }

        public async Task DeleteSkillsForProjectAsync(int projectId)
        {
            // var requiredSkills = await _context.RequiredSkills.Where(rs => rs.ProjectId == projectId && !rs.IsDeleted).ToListAsync();
            
            var project = await _context.Projects.Include(p => p.RequiredSkills).FirstOrDefaultAsync(p => p.Id == projectId);

            if (project != null) project.RequiredSkills.Clear();

            await _context.SaveChangesAsync();
        }

       

        public async Task<List<Skill>> GetSkillsForProjectAsync(int projectId)
        {
            var project = await _context.Projects.Include(p => p.RequiredSkills).FirstOrDefaultAsync(p => p.Id == projectId && !p.IsDeleted);

            return project.RequiredSkills;
        }

        public async Task DeleteSkillForProjectAsync(int projectId, int skillId)
        {
            var project = await _context.Projects.Include(p => p.RequiredSkills).FirstOrDefaultAsync(p => p.Id == projectId);
            var skill = await _context.Skills.FirstOrDefaultAsync(p => p.Id == skillId);
            if (project != null && skill != null) project.RequiredSkills.Remove(skill);
            await _context.SaveChangesAsync();
        }
    }

}
