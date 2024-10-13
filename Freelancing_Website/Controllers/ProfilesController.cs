using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Freelancing_Website.Interfaces;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;
using CodeSphere.Domain.Models;
using Profile = CodeSphere.Domain.Models.Profile;
using CodeSphere.Domain.Interfaces.Repos;

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


        [HttpGet("{profileId}/portfolio")]
        public async Task<IActionResult> GetPortfolioItems(int profileId, int pageNumber = 1, int pageSize = 10)
        {
            var items = await _profileService.GetPortfolioItemsAsync(profileId, pageNumber, pageSize);
            var itemsView = _mapper.Map<List<PortfolioItemView>>(items.Items);
            DataWithPagination<PortfolioItemView> data = new DataWithPagination<PortfolioItemView>();
            data.Items = itemsView;
            data.PaginationMetaData = items.PaginationMetaData;
            return Ok(data);
        }

       

        [HttpPost("{profileId}/portfolio")]
        public async Task<IActionResult> CreatePortfolioItem(int profileId, [FromBody] PortfolioItemForCreate itemForCreate)
        {
            var portfolioItem = _mapper.Map<PortfolioItem>(itemForCreate);
            var createdItem = await _profileService.CreatePortfolioItemAsync(profileId, portfolioItem);
            var itemView = _mapper.Map<PortfolioItemView>(createdItem);
            return CreatedAtAction(nameof(GetPortfolioItems), new { profileId }, itemView);
        }

       
        [HttpPut("portfolio/{id}")]
        public async Task<IActionResult> UpdatePortfolioItem(int id,[FromBody] PortfolioItemForCreate item)
        {
            var portfolioItem=await _profileService.GetPortfolioItemByIdAsync(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }
            portfolioItem.Title=item.Title;
            portfolioItem.Description=item.Description;
            portfolioItem.ImageUrl=item.ImageUrl;

            await _profileService.UpdatePortfolioItemAsync(portfolioItem);
            return NoContent();
        }

        
        [HttpDelete("portfolio/{itemId}")]
        public async Task<IActionResult> DeletePortfolioItem(int itemId)
        {
            await _profileService.DeletePortfolioItemAsync(itemId);
            return NoContent();
        }

        [HttpGet("{portfolioItemId}")]
        public async Task<ActionResult> GetPortfolioItemById(int portfolioItemId)
        {
            var portfolioItem = await _profileService.GetPortfolioItemByIdAsync(portfolioItemId);
            var portfolioItemView = _mapper.Map<PortfolioItemView>(portfolioItem);
            return Ok(portfolioItemView);
        }
    }
}
