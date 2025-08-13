using CodeSphere.Domain.Models;

namespace Freelancing_Website.Models.ViewModels
{
    public class FreelancerView : UserView
    {
        public List<ProjectView> CompletedProjects { get; set; }
        public List<BidView> Bids { get; set; }
        public List<ReviewView> ReviewsReceived { get; set; }
        public int ProfileId { get; set; }
        public ProfileView Profile { get; set; }

        public FreelancerView()
        {
            CompletedProjects = new List<ProjectView>();
            Bids = new List<BidView>();
            ReviewsReceived = new List<ReviewView>();

        }
    }
}
