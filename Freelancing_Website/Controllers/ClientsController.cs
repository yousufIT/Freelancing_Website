using AutoMapper;
using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Freelancing_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(IClientRepository clientRepository, IMapper mapper, ILogger<ClientsController> logger)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var (clients, paginationMetaData) = await _clientRepository.GetAllAsync(pageNumber, pageSize);
                return Ok(_mapper.Map<List<ClientView>>(clients));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching clients");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(id);
                if (client == null) return NotFound();
                return Ok(_mapper.Map<ClientView>(client));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching client with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientForCreate clientForCreate)
        {
            try
            {
                if (clientForCreate == null) return BadRequest("Client is null");
                var client = _mapper.Map<Client>(clientForCreate);
                await _clientRepository.AddAsync(client);
                return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, _mapper.Map<ClientView>(client));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating client");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientForCreate clientForCreate)
        {
            try
            {
                var existingClient = await _clientRepository.GetByIdAsync(id);
                if (existingClient == null) return NotFound();

                existingClient.Name = clientForCreate.Name;
                existingClient.Email = clientForCreate.Email;
                existingClient.Rating = clientForCreate.Rating;
                existingClient.Role = clientForCreate.Role;
                existingClient.CompanyName = clientForCreate.CompanyName;
                existingClient.ContactNumber = clientForCreate.ContactNumber;

                await _clientRepository.UpdateAsync(existingClient);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating client with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                await _clientRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting client with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
