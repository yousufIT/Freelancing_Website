using Freelancing_Website.Models.ViewModels;
using System.Threading.Tasks;

namespace Freelancing_Website.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryView> GetDashboardSummaryAsync(int recentCount = 5);
    }
}
