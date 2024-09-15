using CodeSphere.Domain.Models;
using CodeSphere.Infrastructure.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Infrastructure.Repos
{
    public class ProfileRepository : Repository<Profile>
    {
        public ProfileRepository(CodeSphereContext _context, ILogger<Repository<Profile>> _logger)
        : base(_context, _logger)
        {
        }
    }
}
