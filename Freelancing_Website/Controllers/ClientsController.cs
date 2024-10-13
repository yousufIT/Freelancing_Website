using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Freelancing_Website.Interfaces;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;
using CodeSphere.Domain.Models;
using CodeSphere.Domain.Interfaces.Repos;
using Microsoft.AspNetCore.Identity;

namespace Freelancing_Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public ClientsController(IClientService clientService, IMapper mapper, UserManager<User> userManager)
        {
            _clientService = clientService;
            _mapper = mapper;
            _userManager = userManager;
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
            var client = await _clientService.GetClientByIdAsync(id);
            if ( client == null)
            {
                return NotFound();
            }
            client.CompanyName = clientForCreate.CompanyName;
            client.ContactNumber = clientForCreate.ContactNumber;
            client.Name = clientForCreate.Name;
            client.UserName = clientForCreate.UserName;
            client.Email = clientForCreate.Email;
            client.Role = clientForCreate.Role;
            client.Rating = clientForCreate.Rating;
            await _clientService.UpdateClientAsync(client);
            await _userManager.UpdateSecurityStampAsync(client);
            var clientViewModel = _mapper.Map<ClientView>(client);
            return Ok(clientViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientService.DeleteClientAsync(id);
            return NoContent();
        }
    }
}
