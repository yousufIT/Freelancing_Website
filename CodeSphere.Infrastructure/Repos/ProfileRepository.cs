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
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        public ProfileRepository(CodeSphereContext context, ILogger<Repository<Profile>> logger)
            : base(context, logger)
        {
        }

        public async Task<Profile> GetProfileByFreelancerIdAsync(int freelancerId)
        {
            return await _context.Profiles
                .FirstOrDefaultAsync(profile => profile.FreelancerId == freelancerId && !profile.IsDeleted);
        }

        public async Task DeleteByFreelancerIdAsync(int freelancerId)
        {
            var profile = await GetProfileByFreelancerIdAsync(freelancerId);
            if (profile != null)
            {
                profile.IsDeleted = true; // Soft delete
                await UpdateAsync(profile);
            }
        }
    }

}
