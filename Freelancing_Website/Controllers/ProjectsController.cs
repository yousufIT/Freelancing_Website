using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Freelancing_Website.Interfaces;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models;
using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ViewModels;
using CodeSphere.Domain.Interfaces.Repos;
using Microsoft.AspNetCore.Authorization;

namespace Freelancing_Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet("Skill/{Ids}")]
        public async Task<IActionResult> GetProjectsFilteredBySkills( string Ids, int pageNumber = 1, int pagesize = 10)
        {
            List<int> skillsIds= Ids.Split(',') 
                      .Select(int.Parse) 
                      .ToList();
            var projects = await _projectService.GetProjectsBySkills(skillsIds, pageNumber, pagesize);
            var projectsViews = _mapper.Map<List<ProjectView>>(projects.Items);
            DataWithPagination<ProjectView> dataWithPagination = new DataWithPagination<ProjectView>();
            dataWithPagination.Items = projectsViews;
            dataWithPagination.PaginationMetaData = projects.PaginationMetaData;
            return Ok(dataWithPagination);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            var projectView = _mapper.Map<ProjectView>(project);
            return Ok(projectView);
        }

        [Authorize(Roles = "Client")]
        [HttpPost("Client/{clientId}")]
        public async Task<IActionResult> CreateProject(int clientId,[FromBody] ProjectForCreate projectForCreate)
        {
            var project = _mapper.Map<Project>(projectForCreate);
            await _projectService.CreateProjectAsync(clientId,project);
            var projectView = _mapper.Map<ProjectView>(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, projectView);
        }

		[Authorize(Roles = "Client")]
		[HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectForCreate projectForCreate)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project==null)
            {
                return NotFound();
            }
            project.Title = projectForCreate.Title;
            project.Description = projectForCreate.Description;
            project.Budget = projectForCreate.Budget;
            project.Status = projectForCreate.Status;
            await _projectService.UpdateProjectAsync(project);
            var projectView = _mapper.Map<ProjectView>(project);
            return Ok(projectView);
        }

		[Authorize(Roles = "Client")]
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }
    }
}
