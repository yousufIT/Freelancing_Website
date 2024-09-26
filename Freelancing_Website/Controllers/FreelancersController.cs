using AutoMapper;
using CodeSphere.Domain.Interfaces;
using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Freelancing_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancersController : ControllerBase
    {
        private readonly IRepository<Freelancer> _freelancerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<FreelancersController> _logger;

        public FreelancersController(IRepository<Freelancer> freelancerRepository, IMapper mapper, ILogger<FreelancersController> logger)
        {
            _freelancerRepository = freelancerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFreelancers(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var (freelancers, paginationMetaData) = await _freelancerRepository.GetAllAsync(pageNumber, pageSize);
                return Ok(_mapper.Map<List<FreelancerView>>(freelancers));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching freelancers");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFreelancerById(int id)
        {
            try
            {
                var freelancer = await _freelancerRepository.GetByIdAsync(id);
                if (freelancer == null) return NotFound();
                return Ok(_mapper.Map<FreelancerView>(freelancer));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching freelancer with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFreelancer([FromBody] FreelancerForCreate freelancerForCreate)
        {
            try
            {
                if (freelancerForCreate == null) return BadRequest("Freelancer is null");
                var freelancer = _mapper.Map<Freelancer>(freelancerForCreate);
                await _freelancerRepository.AddAsync(freelancer);
                return CreatedAtAction(nameof(GetFreelancerById), new { id = freelancer.Id }, _mapper.Map<FreelancerView>(freelancer));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating freelancer");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFreelancer(int id, [FromBody] FreelancerForCreate freelancerForCreate)
        {
            try
            {
                var existingFreelancer = await _freelancerRepository.GetByIdAsync(id);
                if (existingFreelancer == null) return NotFound();

                existingFreelancer.Name = freelancerForCreate.Name;
                existingFreelancer.Email = freelancerForCreate.Email;
                existingFreelancer.Rating = freelancerForCreate.Rating;
                existingFreelancer.Role = freelancerForCreate.Role;
                existingFreelancer.Skills = freelancerForCreate.Skills;
                existingFreelancer.Hourlysalary = freelancerForCreate.Hourlysalary;

                await _freelancerRepository.UpdateAsync(existingFreelancer);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating freelancer with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFreelancer(int id)
        {
            try
            {
                await _freelancerRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting freelancer with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
