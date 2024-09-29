using AutoMapper;
using CodeSphere.Domain.Interfaces;
using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using CodeSphere.Infrastructure.Repos;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freelancing_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<ProjectsController> _logger;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectRepository projectRepository, ILogger<ProjectsController> logger,IMapper mapper)
        {
            _projectRepository = projectRepository;
            _logger = logger;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects(int pageNumber = 1, int pageSize = 10,List<string> filterSkills = null)
        {
            List<Project> projects;
            PaginationMetaData paginationMetaData;

            if (filterSkills == null)
            {
                _logger.LogInformation("Fetching all projects.");
                (projects, paginationMetaData) = await _projectRepository.GetAllAsync(pageNumber, pageSize);
            }
            else
            {
                _logger.LogInformation("Fetching all the projects that match filterskills.");
                (projects, paginationMetaData) = await _projectRepository.Search(pageNumber, pageSize,filterSkills);
            }
            return Ok(_mapper.Map<List<ProjectView>>(projects));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            _logger.LogInformation($"Fetching project with id {id}");
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProjectView>(project));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectForCreate project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Creating new project.");
            var newProject = _mapper.Map<Project>(project);
            await _projectRepository.AddAsync(newProject);
            return CreatedAtAction(nameof(GetProject), new { id = ((IBase)newProject).Id }, _mapper.Map<ProjectView>(newProject));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectForCreate project)
        {
            var oldProject = await _projectRepository.GetByIdAsync(id);
            if (oldProject == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Updating project with id {id}");
            
            oldProject.Title = project.Title;
            oldProject.Description = project.Description;
            oldProject.Status = project.Status;
            oldProject.Budget = project.Budget;

            await _projectRepository.UpdateAsync(oldProject);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            _logger.LogInformation($"Deleting project with id {id}");
            await _projectRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
