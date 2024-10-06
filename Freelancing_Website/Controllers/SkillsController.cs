using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Freelancing_Website.Interfaces;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models;
using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ViewModels;
using CodeSphere.Domain.Interfaces.Repos;
using Freelancing_Website.Services;

namespace Freelancing_Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;
        private readonly IMapper _mapper;

        public SkillsController(ISkillService skillService, IMapper mapper)
        {
            _skillService = skillService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSkills(int id, int pageNumber = 1, int pageSize = 10)
        {
            var skills = await _skillService.GetSkillsForFreelancerAsync(id, pageNumber, pageSize);
            var skillViews = _mapper.Map<List<SkillView>>(skills.Items);
            DataWithPagination<SkillView> skillsWithPagination = new DataWithPagination<SkillView>();
            skillsWithPagination.Items = skillViews;
            skillsWithPagination.PaginationMetaData = skills.PaginationMetaData;
            return Ok(skillsWithPagination);
        }
        [HttpGet("Freelancer/{freelancerId}")]
        public async Task<IActionResult> GetSkillsForFreelancer(int freelancerId, int pageNumber = 1, int pageSize = 10)
        {
            var skills = await _skillService.GetSkillsForFreelancerAsync(freelancerId, pageNumber,pageSize);
            var skillViews = _mapper.Map<List<SkillView>>(skills.Items);
            DataWithPagination<SkillView> skillsWithPagination = new DataWithPagination<SkillView>();
            skillsWithPagination.Items = skillViews;
            skillsWithPagination.PaginationMetaData = skills.PaginationMetaData;
            return Ok(skillsWithPagination);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkillById(int id)
        {
            var skill = await _skillService.GetSkillByIdAsync(id);
            if (skill == null)
            {
                return NotFound();
            }
            var skillView = _mapper.Map<SkillView>(skill);
            return Ok(skillView);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill([FromBody] SkillForCreate skillForCreate)
        {
            var skill = _mapper.Map<Skill>(skillForCreate);
            await _skillService.CreateSkillAsync(skill);
            var skillView = _mapper.Map<SkillView>(skill);
            return CreatedAtAction(nameof(GetSkillById), new { id = skill.Id }, skillView);
        }
        [HttpPost("Freelancer/{freelancerId}")]
        public async Task<IActionResult> CreateSkillsForFreelancer(int freelancerId,[FromBody] List<SkillForCreate> skillsForCreate)
        {
            var skills = _mapper.Map<List<Skill>>(skillsForCreate);
            await _skillService.CreateSkillsToFreelancerAsync(freelancerId, skills);
            return CreatedAtAction(nameof(GetSkillsForFreelancer), new { freelancerId }, _mapper.Map<List<SkillView>>(skills));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(int id, [FromBody] SkillForCreate skillForCreate)
        {
            var skill = _mapper.Map<Skill>(skillForCreate);
            if (id != skill.Id)
            {
                return BadRequest();
            }
            await _skillService.UpdateSkillAsync(skill);
            var skillView = _mapper.Map<SkillView>(skill);
            return Ok(skillView);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            await _skillService.DeleteSkillAsync(id);
            return NoContent();
        }
    }
}
