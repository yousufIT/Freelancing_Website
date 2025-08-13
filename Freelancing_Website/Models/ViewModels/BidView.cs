using System.ComponentModel.DataAnnotations;

namespace Freelancing_Website.Models.ViewModels
{
    public class BidView
    {
        public int Id { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive number")]
        public double Amount { get; set; }
        public string Proposal { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int FreelancerId { get; set; }
    }
}
