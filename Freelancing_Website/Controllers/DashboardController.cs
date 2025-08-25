using Microsoft.AspNetCore.Mvc;
using Freelancing_Website.Interfaces;
using Freelancing_Website.Models.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Freelancing_Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("summary")]
        public async Task<ActionResult<DashboardSummaryView>> GetSummary()
        {
            var summary = await _dashboardService.GetDashboardSummaryAsync(5);
            return Ok(summary);
        }
    }
}
