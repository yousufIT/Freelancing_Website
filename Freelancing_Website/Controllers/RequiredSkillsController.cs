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
    public class RequiredSkillsController : ControllerBase
    {
        private readonly IRequiredSkillService _requiredSkillService;
        private readonly IMapper _mapper;

        public RequiredSkillsController(IRequiredSkillService requiredSkillService, IMapper mapper)
        {
            _requiredSkillService = requiredSkillService;
            _mapper = mapper;
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetSkillsForProject(int projectId,int pageNumber = 1, int pageSize = 10)
        {
            var skills = await _requiredSkillService.GetSkillsForProjectAsync(projectId,pageNumber,pageSize);
            var skillViews = _mapper.Map<IEnumerable<RequiredSkillView>>(skills);
            return Ok(skillViews);
        }

        [HttpPost("project/{projectId}")]
        public async Task<IActionResult> AddSkillsToProject(int projectId, [FromBody] List<RequiredSkillForCreate> skills)
        {
            var requiredSkills = _mapper.Map<List<RequiredSkill>>(skills);
            await _requiredSkillService.AddSkillsToProjectAsync(projectId, requiredSkills);
            return CreatedAtAction(nameof(GetSkillsForProject), new { projectId }, requiredSkills);
        }

        [HttpPut("project/{projectId}")]
        public async Task<IActionResult> UpdateSkillsForProject(int projectId, [FromBody] List<RequiredSkillForCreate> skills)
        {
            var requiredSkills = _mapper.Map<List<RequiredSkill>>(skills);
            await _requiredSkillService.UpdateSkillsForProjectAsync(projectId, requiredSkills);
            return NoContent();
        }

        [HttpDelete("project/{projectId}/skill/{skillId}")]
        public async Task<IActionResult> RemoveSkillFromProject(int projectId, int skillId)
        {
            await _requiredSkillService.RemoveSkillFromProjectAsync(projectId, skillId);
            return NoContent();
        }
    }
}
