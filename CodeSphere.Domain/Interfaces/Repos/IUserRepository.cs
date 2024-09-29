using CodeSphere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Interfaces.Repos
{
    public interface IUserRepository : IRepository<User>
    {
        //Task<User> GetUserByEmailAsync(string email);
    }
}
