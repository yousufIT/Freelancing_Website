using CodeSphere.Domain.Models;

namespace Freelancing_Website.Models.ViewModels
{
    public class ClientView : UserView
    {
        public List<Project> PostedProjects { get; set; }
        public List<Review> ReviewsGiven { get; set; }
        public string CompanyName { get; set; }
        public string ContactNumber { get; set; }

        public ClientView()
        {
            PostedProjects = new List<Project>();
            ReviewsGiven = new List<Review>();

        }
    }
}
