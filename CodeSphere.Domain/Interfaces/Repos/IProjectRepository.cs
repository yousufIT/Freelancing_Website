﻿using CodeSphere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Interfaces.Repos
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<DataWithPagination<Project>> GetProjectsByClientIdAsync(int clientId, int pageNumber, int pageSize);
        Task DeleteProjectsByClientIdAsync(int clientId);
        Task AddProjectToClient(int clientId, Project project);
        Task<DataWithPagination<Project>> GetProjectsBySkillsAsync(List<int> skillsIds, int pageSize, int pageNumber);
        Task<Project> GetProjectById(int id);
    }


}
