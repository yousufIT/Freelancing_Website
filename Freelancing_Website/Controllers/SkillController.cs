using AutoMapper;
using CodeSphere.Domain.Interfaces;
using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Freelancing_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly IRepository<Skill> _skillRepository;
        private readonly ILogger<SkillController> _logger;
        private readonly IMapper _mapper;

        public SkillController(IRepository<Skill> skillRepository, ILogger<SkillController> logger, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSkills(int pageNumber = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching all skills.");
            var (skills, paginationMetaData) = await _skillRepository.GetAllAsync(pageNumber, pageSize);
            return Ok(_mapper.Map<List<SkillView>>(skills));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkill(int id)
        {
            _logger.LogInformation($"Fetching skill with id {id}");
            var skill = await _skillRepository.GetByIdAsync(id);
            if (skill == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SkillView>(skill));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill([FromBody] SkillForCreate skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Creating new skill.");
            var newSkill = _mapper.Map<Skill>(skill);
            await _skillRepository.AddAsync(newSkill);
            return CreatedAtAction(nameof(GetSkill), new { id = newSkill.Id }, _mapper.Map<SkillView>(newSkill));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(int id, [FromBody] SkillForCreate skill)
        {
            var oldSkill = await _skillRepository.GetByIdAsync(id);
            if (oldSkill == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Updating skill with id {id}");

            oldSkill.Name = skill.Name;

            await _skillRepository.UpdateAsync(oldSkill);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            _logger.LogInformation($"Deleting skill with id {id}");
            await _skillRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}