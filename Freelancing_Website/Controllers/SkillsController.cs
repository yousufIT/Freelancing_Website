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
        public async Task<IActionResult> GetAllSkills()
        {
            var skills = await _skillService.GetAllSkillsAsync();
            var skillViews = _mapper.Map<List<SkillView>>(skills);
            return Ok(skillViews);
        }
        [HttpGet("Freelancer/{freelancerId}")]
        public async Task<IActionResult> GetSkillsForFreelancer(int freelancerId)
        {
            var skills = await _skillService.GetSkillsForFreelancerAsync(freelancerId);
            var skillViews = _mapper.Map<List<SkillView>>(skills);
            return Ok(skillViews);
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
        public async Task<IActionResult> SetSkillsForFreelancer(int freelancerId, [FromBody] List<int> skillsIds)
        {
            await _skillService.ReplaceSkillsForFreelancerAsync(freelancerId, skillsIds);
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(int id, [FromBody] SkillForCreate skillForCreate)
        {
            var skill = await _skillService.GetSkillByIdAsync(id);
            if ( skill==null)
            {
                return NotFound();
            }
            skill.Name = skillForCreate.Name;
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
