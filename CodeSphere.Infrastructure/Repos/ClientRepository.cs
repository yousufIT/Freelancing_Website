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
    public class ClientRepository : Repository<Client>
    {
        public ClientRepository(CodeSphereContext context, ILogger<Repository<Client>> logger)
            : base(context, logger)
        {
        }
    }
}
