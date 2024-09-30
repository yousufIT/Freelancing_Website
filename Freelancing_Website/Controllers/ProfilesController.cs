using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Freelancing_Website.Interfaces;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;
using CodeSphere.Domain.Models;
using Profile = CodeSphere.Domain.Models.Profile;

namespace Freelancing_Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;

        public ProfilesController(IProfileService profileService, IMapper mapper)
        {
            _profileService = profileService;
            _mapper = mapper;
        }

        [HttpGet("{profileId}")]
        public async Task<IActionResult> GetProfile(int profileId)
        {
            var profile = await _profileService.GetProfileAsync(profileId);
            if (profile == null) return NotFound();

            var profileView = _mapper.Map<ProfileView>(profile);
            return Ok(profileView);
        }

        [HttpGet("{profileId}/portfolio")]
        public async Task<IActionResult> GetPortfolioItems(int profileId, int pageNumber = 1, int pageSize = 10)
        {
            var items = await _profileService.GetPortfolioItemsAsync(profileId, pageNumber, pageSize);
            var itemsView = _mapper.Map<List<PortfolioItemView>>(items.Items); 
            return Ok(new { items = itemsView,count = items.Items.Count() });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] ProfileForCreate profileForCreate)
        {
            var profile = _mapper.Map<Profile>(profileForCreate);
            var createdProfile = await _profileService.CreateProfileAsync(profile);
            var profileView = _mapper.Map<ProfileView>(createdProfile);
            return CreatedAtAction(nameof(GetProfile), new { profileId = profileView.Id }, profileView);
        }

        [HttpPost("{profileId}/portfolio")]
        public async Task<IActionResult> CreatePortfolioItem(int profileId, [FromBody] PortfolioItemForCreate itemForCreate)
        {
            var portfolioItem = _mapper.Map<PortfolioItem>(itemForCreate);
            var createdItem = await _profileService.CreatePortfolioItemAsync(profileId, portfolioItem);
            var itemView = _mapper.Map<PortfolioItemView>(createdItem);
            return CreatedAtAction(nameof(GetPortfolioItems), new { profileId }, itemView);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileForCreate profileForCreate)
        {
            var profile = _mapper.Map<Profile>(profileForCreate);
            await _profileService.UpdateProfileAsync(profile);
            return NoContent();
        }

        [HttpPut("portfolio")]
        public async Task<IActionResult> UpdatePortfolioItem([FromBody] PortfolioItem item)
        {
            await _profileService.UpdatePortfolioItemAsync(item);
            return NoContent();
        }

        [HttpDelete("{profileId}")]
        public async Task<IActionResult> DeleteProfile(int profileId)
        {
            await _profileService.DeleteProfileAsync(profileId);
            return NoContent();
        }

        [HttpDelete("portfolio/{itemId}")]
        public async Task<IActionResult> DeletePortfolioItem(int itemId)
        {
            await _profileService.DeletePortfolioItemAsync(itemId);
            return NoContent();
        }
    }
}
