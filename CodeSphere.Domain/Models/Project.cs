using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Budget { get; set; }
        public string Status { get; set; } 
        public int ClientId { get; set; }
        public User Client { get; set; } 
         public ICollection<Bid> Bids { get; set; }
        public ICollection<RequiredSkill> RequiredSkills { get; set; }
        public int? SelectedFreelancerId { get; set; }
        public User SelectedFreelancer { get; set; }
    }
}
