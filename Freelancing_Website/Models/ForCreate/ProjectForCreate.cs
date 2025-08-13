using System.ComponentModel.DataAnnotations;

namespace Freelancing_Website.Models.ForCreate
{
    public class ProjectForCreate
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive number")]
        public double Budget { get; set; }
        public string Status { get; set; }
        public int ClientId { get; set; }
    }
}
