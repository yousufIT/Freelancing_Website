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
    public class BidRepository : Repository<Bid>
    {
        public BidRepository(CodeSphereContext _context, ILogger<Repository<Bid>> _logger)
        : base(_context, _logger)
        {
        }
    }
}
