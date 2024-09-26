using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Models
{
    public class Freelancer : User
    {
        public List<Skill> Skills { get; set; }
        public List<Project> CompletedProjects { get; set; }
        public List<Bid> Bids { get; set; }
        public List<Review> ReviewsReceived { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public double Hourlysalary { get; set; }

        public Freelancer()
        {
            Skills = new List<Skill>();
            CompletedProjects = new List<Project>();
            Bids= new List<Bid>();
            ReviewsReceived = new List<Review>();

        }
    }
}
