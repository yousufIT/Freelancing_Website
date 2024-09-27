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
    public class ProjectRepository : Repository<Project>
    {
        public ProjectRepository(CodeSphereContext _context, ILogger<Repository<Project>> _logger)
        : base(_context, _logger)
        {
        }

        public async Task<(List<Project>,PaginationMetaData)> Search(int pageNumber, int pageSize, List<string> filterWords )
        {
            var totalItemCount = await _context.Set<Project>()
        .Where(e => !e.IsDeleted && filterWords.All(fw => e.RequiredSkills.Any(rs => rs.Name.Contains(fw))))
        .CountAsync();

            var paginationData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var response = await _context.Set<Project>()
                .Where(e => !e.IsDeleted && filterWords.All(fw => e.RequiredSkills.Any(rs => rs.Name.Contains(fw))))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (response, paginationData);
        }
    }
}
