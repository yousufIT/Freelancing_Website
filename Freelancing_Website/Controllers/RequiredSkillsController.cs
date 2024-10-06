using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Freelancing_Website.Interfaces;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models;
using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ViewModels;
using CodeSphere.Domain.Interfaces.Repos;

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
            var skillViews = _mapper.Map<List<RequiredSkillView>>(skills.Items);
            DataWithPagination<RequiredSkillView> data = new DataWithPagination<RequiredSkillView>();
            data.Items = skillViews;
            data.PaginationMetaData = skills.PaginationMetaData;
            return Ok(data);
        }

        [HttpPost("project/{projectId}")]
        public async Task<IActionResult> AddSkillsToProject(int projectId, [FromBody] List<RequiredSkillForCreate> skills)
        {
            var requiredSkills = _mapper.Map<List<RequiredSkill>>(skills);
            await _requiredSkillService.AddSkillsToProjectAsync(projectId, requiredSkills);
            return CreatedAtAction(nameof(GetSkillsForProject), new { projectId }, _mapper.Map<List<RequiredSkillView>>(requiredSkills));
        }

        [HttpPut("{skillId}")]
        public async Task<IActionResult> UpdateSkillsForProject(int skillId, [FromBody] RequiredSkillForCreate skill)
        {
            var requiredSkill = await _requiredSkillService.GetSkillByIdAsync(skillId);
            if(requiredSkill==null)
            {
                return NotFound();
            }
            requiredSkill.Name = skill.Name;
            await _requiredSkillService.UpdateSkillForProjectAsync(requiredSkill);
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
