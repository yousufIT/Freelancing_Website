using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Freelancing_Website.Interfaces;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;
using CodeSphere.Domain.Models;
using CodeSphere.Domain.Interfaces.Repos;

namespace Freelancing_Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FreelancersController : ControllerBase
    {
        private readonly IFreelancerService _freelancerService;
        private readonly IMapper _mapper;

        public FreelancersController(IFreelancerService freelancerService, IMapper mapper)
        {
            _freelancerService = freelancerService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFreelancerById(int id)
        {
            var freelancer = await _freelancerService.GetFreelancerByIdAsync(id);
            if (freelancer == null)
            {
                return NotFound();
            }
            var freelancerViewModel = _mapper.Map<FreelancerView>(freelancer);
            return Ok(freelancerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFreelancer([FromBody] FreelancerForCreate freelancerForCreate)
        {
            var freelancer = _mapper.Map<Freelancer>(freelancerForCreate);
            await _freelancerService.CreateFreelancerAsync(freelancer);
            var freelancerViewModel = _mapper.Map<FreelancerView>(freelancer);
            return CreatedAtAction(nameof(GetFreelancerById), new { id = freelancer.Id }, freelancerViewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFreelancer(int id, [FromBody] FreelancerForCreate freelancerForCreate)
        {
            var freelancer = await _freelancerService
                .GetFreelancerByIdAsync(id);
            if (freelancer==null)
            {
                return NotFound();
            }
            freelancer.Hourlysalary = freelancerForCreate.Hourlysalary;
            freelancer.Profile.Bio = freelancerForCreate.Profile.Bio;
            freelancer.Name = freelancerForCreate.Name;
            freelancer.UserName = freelancerForCreate.UserName;
            freelancer.Email = freelancerForCreate.Email;
            freelancer.PasswordHash = freelancerForCreate.PasswordHash;
            freelancer.Role = freelancerForCreate.Role;
            freelancer.Rating = freelancerForCreate.Rating;
            await _freelancerService.UpdateFreelancerAsync(freelancer);
            var freelancerViewModel = _mapper.Map<FreelancerView>(freelancer);
            return Ok(freelancerViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFreelancer(int id)
        {
            await _freelancerService.DeleteFreelancerAsync(id);
            return NoContent();
        }

    }
}
