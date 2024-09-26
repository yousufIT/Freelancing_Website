using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Models
{
    public class Client : User
    {
        public List<Project> PostedProjects { get; set; }
        public List<Review> ReviewsGiven { get; set; }
        public string CompanyName { get; set; }
        public string ContactNumber { get; set; }

        public Client()
        {
            PostedProjects = new List<Project>();
            ReviewsGiven = new List<Review>();

        }
    }
}
