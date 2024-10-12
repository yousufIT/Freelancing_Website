using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ForCreate;

namespace Freelancing_Website.Models.ViewModels
{
    public class ReviewView
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int FreelancerId { get; set; }
        public string FreelancerName { get;set; }

    }
}
