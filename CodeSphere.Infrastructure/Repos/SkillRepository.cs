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

        public async Task AddSkillToFreelancerAsync(int freelancerId, Skill skill)
        {
            var freelancer = await _context.Freelancers
                 .Include(f => f.Profile)
                 .ThenInclude(p=>p.Skills)
                 .FirstOrDefaultAsync(f => f.Id == freelancerId);
            var profile=freelancer.Profile;
            profile.Skills.Add(skill);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSkillsForFreelancerAsync(int freelancerId, List<Skill> skills)
        {
            var freelancer = await _context.Freelancers
                 .Include(f => f.Profile)
                 .ThenInclude(p => p.Skills)
                 .FirstOrDefaultAsync(f => f.Id == freelancerId);
            freelancer.Profile.Skills = skills;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSkillsForFreelancerAsync(int freelancerId)
        {
            /*var skills = await _context.Skills
                .Where(skill => skill.Profiles.Any(profile => profile.FreelancerId == freelancerId) && !skill.IsDeleted)
                .ToListAsync();*/

            var freelancer = await _context.Freelancers.Include(f => f.Profile).ThenInclude(p => p.Skills).FirstOrDefaultAsync(f => f.Id == freelancerId);

            if(freelancer != null) freelancer.Profile.Skills.Clear();

            await _context.SaveChangesAsync();
        }

        public async Task<List<Skill>> GetSkillsForFreelancerAsync(int freelancerId)
        {
            var freelancer = await _context.Freelancers
                 .Include(f => f.Profile)
                 .ThenInclude(p => p.Skills)
                 .FirstOrDefaultAsync(f => f.Id == freelancerId);
            var profile=freelancer.Profile;
            var skills = profile.Skills;
            return skills;
        }
    }

}
