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
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientsController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            var clientViewModel = _mapper.Map<ClientView>(client);
            return Ok(clientViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientForCreate clientForCreate)
        {
            var client = _mapper.Map<Client>(clientForCreate);
            await _clientService.CreateClientAsync(client);
            var clientViewModel = _mapper.Map<ClientView>(client);
            return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, clientViewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientForCreate clientForCreate)
        {
            var client = _mapper.Map<Client>(clientForCreate);
            if (id != client.Id)
            {
                return BadRequest();
            }
            await _clientService.UpdateClientAsync(client);
            var clientViewModel = _mapper.Map<ClientView>(client);
            return Ok(clientViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientService.DeleteClientAsync(id);
            return NoContent();
        }

        [HttpGet("{clientId}/reviews")]
        public async Task<IActionResult> GetReviewsForClient(int clientId, int pageNumber = 1, int pageSize = 10)
        {
            var reviews = await _clientService.GetReviewsForClientAsync(clientId, pageNumber, pageSize);
            var reviewViewModels = _mapper.Map<List<ReviewView>>(reviews.Items);
            DataWithPagination<ReviewView> reviewsWithPagination = new DataWithPagination<ReviewView>();
            reviewsWithPagination.Items = reviewViewModels;
            reviewsWithPagination.PaginationMetaData = reviews.PaginationMetaData;
            return Ok(reviewsWithPagination);
        }
    }
}
