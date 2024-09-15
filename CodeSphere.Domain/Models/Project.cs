using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Models
{
    public class Project : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Budget { get; set; }
        public string Status { get; set; } 
        public int ClientId { get; set; }
        public User Client { get; set; } 
         public List<Bid> Bids { get; set; }
        public List<RequiredSkill> RequiredSkills { get; set; }
        public int? SelectedFreelancerId { get; set; }
        public User SelectedFreelancer { get; set; }
    }
}
