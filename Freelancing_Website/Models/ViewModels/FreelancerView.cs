using CodeSphere.Domain.Models;

namespace Freelancing_Website.Models.ViewModels
{
    public class FreelancerView : UserView
    {
        public List<Project> CompletedProjects { get; set; }
        public List<Bid> Bids { get; set; }
        public List<Review> ReviewsReceived { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public double Hourlysalary { get; set; }

        public FreelancerView()
        {
            CompletedProjects = new List<Project>();
            Bids = new List<Bid>();
            ReviewsReceived = new List<Review>();

        }
    }
}
