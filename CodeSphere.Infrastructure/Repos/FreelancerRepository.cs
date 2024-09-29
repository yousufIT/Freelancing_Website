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
    public class FreelancerRepository : Repository<Freelancer>, IFreelancerRepository
    {
        public FreelancerRepository(CodeSphereContext context, ILogger<Repository<Freelancer>> logger)
            : base(context, logger)
        {
        }
        public async Task<IEnumerable<Freelancer>> GetFreelancersBySkillAsync(string skill)
        {
            return await _context.Freelancers
                .Include(f => f.Profile)
                .ThenInclude(p => p.Skills)
                .Where(f => f.Profile.Skills.Any(s => s.Name.ToLower() == skill.ToLower()))
                .ToListAsync();
        }

        // Add any custom methods specific to Freelancer here
    }


}
