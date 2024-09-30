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
            skill.Profiles.Add(new Profile { FreelancerId = freelancerId }); 
            await AddAsync(skill);
        }

        public async Task UpdateSkillsForFreelancerAsync(int freelancerId, List<Skill> skills)
        {
            foreach (var skill in skills)
            {
                skill.Profiles.Add(new Profile { FreelancerId = freelancerId }); 
                await UpdateAsync(skill);
            }
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

        public async Task<DataWithPagination<Skill>> GetSkillsForFreelancerAsync(int freelancerId, int pageNumber, int pageSize)
        {
            var skills = await _context.Skills
                .Where(skill => skill.Profiles.Any(profile => profile.FreelancerId == freelancerId) && !skill.IsDeleted)
                .ToListAsync();

            var totalItemCount = skills.Count();

            var paginationData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            DataWithPagination<Skill> result = new DataWithPagination<Skill>();
            result.PaginationMetaData = paginationData;
            result.Items = skills;
            return result;
        }
    }

}
