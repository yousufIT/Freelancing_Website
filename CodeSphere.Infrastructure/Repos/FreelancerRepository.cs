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
    public class FreelancerRepository : Repository<Freelancer>
    {
        public FreelancerRepository(CodeSphereContext context, ILogger<Repository<Freelancer>> logger)
            : base(context, logger)
        {
        }
    }

}
