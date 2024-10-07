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
        
        public async Task<DataWithPagination<Freelancer>> GetFreelancersBySkillAsync(string skill, int pageNumber, int pageSize)
        {
            var freelancers = await _context.Freelancers
                .Include(f => f.Profile)
                .ThenInclude(p => p.Skills)
                .Where(f => f.Profile.Skills.Any(s => s.Name.ToLower() == skill.ToLower()) && !f.IsDeleted)
                .ToListAsync();

            var totalItemCount = freelancers.Count();

            var paginationData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);
            freelancers=freelancers.Skip((pageNumber - 1) * pageSize)
                       .Take(pageSize).ToList();
            DataWithPagination<Freelancer> result = new DataWithPagination<Freelancer>();
            result.PaginationMetaData = paginationData;
            result.Items = freelancers;
            return result;

        }

    }


}
