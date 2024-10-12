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
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(CodeSphereContext context, ILogger<Repository<Project>> logger)
            : base(context, logger) { }

        public async Task AddProjectToClient(int clientId, Project project)
        {
            var client = await _context.Clients
                .Include(c => c.PostedProjects)
                .FirstOrDefaultAsync(c => c.Id == clientId);
            
            project.ClientId = clientId;
            project.Client = client;
            client.PostedProjects.Add(project);
            await AddAsync(project);
        }

        public async Task DeleteProjectsByClientIdAsync(int clientId)
        {
            var projects = await _context.Projects.Where(p => p.ClientId == clientId && !p.IsDeleted).ToListAsync();
            foreach (var project in projects)
            {
                await DeleteAsync(project.Id);
            }
        }

        public async Task<DataWithPagination<Project>> GetProjectsByClientIdAsync(int clientId, int pageNumber, int pageSize)
        {
            var projects = await _context.Projects.Where(p => p.ClientId == clientId && !p.IsDeleted).ToListAsync();

            var totalItemCount = projects.Count();

            var paginationData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);
            projects=projects.Skip((pageNumber - 1) * pageSize)
                       .Take(pageSize).ToList();
            DataWithPagination<Project> result = new DataWithPagination<Project>();
            result.PaginationMetaData = paginationData;
            result.Items = projects;
            return result;
        }

        public async Task<DataWithPagination<Project>> GetProjectsBySkillsAsync(List<int> skillsIds, int pageNumber, int pageSize)
        {
            skillsIds.Remove(0);
            IQueryable<Project> projects ;
            if (skillsIds.Count == 0)
            {
                projects = _context.Projects
                .Include(p => p.RequiredSkills)
                .Where(p => !p.IsDeleted);
            }
            else
            {
                projects = _context.Projects
                .Include(p => p.RequiredSkills)
                .Where(p => !p.IsDeleted && p.RequiredSkills.Any(s => skillsIds.Contains(s.Id)));
            }


            var totalItemCount = projects.Count();

            var paginationData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);
            var projectItems= await projects.Skip((pageNumber - 1) * pageSize)
                       .Take(pageSize).ToListAsync();
            DataWithPagination<Project> result = new DataWithPagination<Project>();
            result.PaginationMetaData = paginationData;
            result.Items = projectItems;
            return result;
        }

    }

}
