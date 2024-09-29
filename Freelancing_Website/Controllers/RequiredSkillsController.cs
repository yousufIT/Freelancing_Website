using AutoMapper;
using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Freelancing_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequiredSkillsController : ControllerBase
    {
        private readonly IRequiredSkillRepository _requiredSkillRepository;
        private readonly ILogger<RequiredSkillsController> _logger;
        private readonly IMapper _mapper;

        public RequiredSkillsController(IRequiredSkillRepository requiredSkillRepository, ILogger<RequiredSkillsController> logger, IMapper mapper)
        {
            _requiredSkillRepository = requiredSkillRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRequiredSkills(int pageNumber = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching all required skills.");
            var (requiredSkills, paginationMetaData) = await _requiredSkillRepository.GetAllAsync(pageNumber, pageSize);
            return Ok(_mapper.Map<List<RequiredSkillView>>(requiredSkills));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequiredSkill(int id)
        {
            _logger.LogInformation($"Fetching required skill with id {id}");
            var requiredSkill = await _requiredSkillRepository.GetByIdAsync(id);
            if (requiredSkill == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RequiredSkillView>(requiredSkill));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequiredSkill([FromBody] RequiredSkillForCreate requiredSkill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Creating new required skill.");
            var newRequiredSkill = _mapper.Map<RequiredSkill>(requiredSkill);
            await _requiredSkillRepository.AddAsync(newRequiredSkill);
            return CreatedAtAction(nameof(GetRequiredSkill), new { id = ((IBase)newRequiredSkill).Id }, _mapper.Map<RequiredSkillView>(newRequiredSkill));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequiredSkill(int id, [FromBody] RequiredSkillForCreate requiredSkill)
        {
            var oldRequiredSkill = await _requiredSkillRepository.GetByIdAsync(id);
            if (oldRequiredSkill == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Updating required skill with id {id}");

            oldRequiredSkill.Name = requiredSkill.Name;
            oldRequiredSkill.ProjectId = requiredSkill.ProjectId;

            await _requiredSkillRepository.UpdateAsync(oldRequiredSkill);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequiredSkill(int id)
        {
            _logger.LogInformation($"Deleting required skill with id {id}");
            await _requiredSkillRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}