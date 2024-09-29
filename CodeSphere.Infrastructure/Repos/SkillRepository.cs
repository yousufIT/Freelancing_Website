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
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        public SkillRepository(CodeSphereContext context, ILogger<SkillRepository> logger)
            : base(context, logger)
        {
        }

        public async Task<List<Skill>> GetSkillsForFreelancerAsync(int freelancerId)
        {
            return await _context.Skills
                .Where(skill => skill.Profiles.Any(profile => profile.FreelancerId == freelancerId) && !skill.IsDeleted)
                .ToListAsync();
        }

        public async Task AddSkillToFreelancerAsync(int freelancerId, Skill skill)
        {
            skill.Profiles.Add(new Profile { FreelancerId = freelancerId }); // Associate skill with freelancer
            await AddAsync(skill);
        }

        public async Task UpdateSkillsForFreelancerAsync(int freelancerId, List<Skill> skills)
        {
            foreach (var skill in skills)
            {
                skill.Profiles.Add(new Profile { FreelancerId = freelancerId }); // Update association
                await UpdateAsync(skill);
            }
        }

        public async Task DeleteSkillsForFreelancerAsync(int freelancerId)
        {
            var skills = await _context.Skills
                .Where(skill => skill.Profiles.Any(profile => profile.FreelancerId == freelancerId) && !skill.IsDeleted)
                .ToListAsync();

            foreach (var skill in skills)
            {
                skill.IsDeleted = true; // Soft delete
                await UpdateAsync(skill);
            }
        }
    }

}
