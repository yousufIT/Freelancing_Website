﻿using CodeSphere.Domain.Interfaces.Repos;
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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(CodeSphereContext context, ILogger<Repository<User>> logger)
            : base(context, logger)
        {
        }
    }
}
