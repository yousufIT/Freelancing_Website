using AutoMapper;
using CodeSphere.Domain.Interfaces;
using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Profile = CodeSphere.Domain.Models.Profile;

namespace Freelancing_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IRepository<Profile> _profileRepository;
        private readonly ILogger<ProfileController> _logger;
        private readonly IMapper _mapper;

        public ProfileController(IRepository<Profile> profileRepository, ILogger<ProfileController> logger, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfiles(int pageNumber = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching all profiles.");
            var (profiles, paginationMetaData) = await _profileRepository.GetAllAsync(pageNumber, pageSize);
            return Ok(_mapper.Map<List<ProfileView>>(profiles));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            _logger.LogInformation($"Fetching profile with id {id}");
            var profile = await _profileRepository.GetByIdAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProfileView>(profile));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] ProfileForCreate profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Creating new profile.");
            var newProfile = _mapper.Map<Profile>(profile);
            await _profileRepository.AddAsync(newProfile);
            return CreatedAtAction(nameof(GetProfile), new { id = newProfile.Id }, _mapper.Map<ProfileView>(newProfile));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] ProfileForCreate profile)
        {
            var oldProfile = await _profileRepository.GetByIdAsync(id);
            if (oldProfile == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Updating profile with id {id}");

            oldProfile.Bio = profile.Bio;

            await _profileRepository.UpdateAsync(oldProfile);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            _logger.LogInformation($"Deleting profile with id {id}");
            await _profileRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}