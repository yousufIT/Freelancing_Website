using CodeSphere.Domain.Models;

namespace Freelancing_Website.Models.ViewModels
{
    public class ClientView : UserView
    {
        public List<ProjectView> PostedProjects { get; set; }
        public List<ReviewView> ReviewsGiven { get; set; }
        public string CompanyName { get; set; }
        public string ContactNumber { get; set; }

        public ClientView()
        {
            PostedProjects = new List<ProjectView>();
            ReviewsGiven = new List<ReviewView>();

        }
    }
}
