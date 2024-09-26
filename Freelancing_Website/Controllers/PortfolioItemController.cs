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
    public class PortfolioItemController : ControllerBase
    {
        private readonly IRepository<PortfolioItem> _portfolioItemRepository;
        private readonly ILogger<PortfolioItemController> _logger;
        private readonly IMapper _mapper;

        public PortfolioItemController(IRepository<PortfolioItem> portfolioItemRepository, ILogger<PortfolioItemController> logger, IMapper mapper)
        {
            _portfolioItemRepository = portfolioItemRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPortfolioItems(int pageNumber = 1, int pageSize = 10)
        {
            _logger.LogInformation("Fetching all portfolio items.");
            var (portfolioItems, paginationMetaData) = await _portfolioItemRepository.GetAllAsync(pageNumber, pageSize);
            return Ok(_mapper.Map<List<PortfolioItemView>>(portfolioItems));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPortfolioItem(int id)
        {
            _logger.LogInformation($"Fetching portfolio item with id {id}");
            var portfolioItem = await _portfolioItemRepository.GetByIdAsync(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PortfolioItemView>(portfolioItem));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePortfolioItem([FromBody] PortfolioItemForCreate portfolioItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Creating new portfolio item.");
            var newPortfolioItem = _mapper.Map<PortfolioItem>(portfolioItem);
            await _portfolioItemRepository.AddAsync(newPortfolioItem);
            return CreatedAtAction(nameof(GetPortfolioItem), new { id = ((IBase)newPortfolioItem).Id }, _mapper.Map<PortfolioItemView>(newPortfolioItem));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePortfolioItem(int id, [FromBody] PortfolioItemForCreate portfolioItem)
        {
            var oldPortfolioItem = await _portfolioItemRepository.GetByIdAsync(id);
            if (oldPortfolioItem == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Updating portfolio item with id {id}");

            oldPortfolioItem.Title = portfolioItem.Title;
            oldPortfolioItem.Description = portfolioItem.Description;

            await _portfolioItemRepository.UpdateAsync(oldPortfolioItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortfolioItem(int id)
        {
            _logger.LogInformation($"Deleting portfolio item with id {id}");
            await _portfolioItemRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}