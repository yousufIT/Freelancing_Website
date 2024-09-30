using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Freelancing_Website.Interfaces;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models;
using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ViewModels;

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

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectForCreate projectForCreate)
        {
            var project = _mapper.Map<Project>(projectForCreate);
            await _projectService.CreateProjectAsync(project);
            var projectView = _mapper.Map<ProjectView>(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, projectView);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectForCreate projectForCreate)
        {
            var project = _mapper.Map<Project>(projectForCreate);
            if (id != project.Id)
            {
                return BadRequest();
            }
            await _projectService.UpdateProjectAsync(project);
            var projectView = _mapper.Map<ProjectView>(project);
            return Ok(projectView);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }
    }
}
