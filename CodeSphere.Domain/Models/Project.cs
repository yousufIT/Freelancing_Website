using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Models
{
    public class Project : IBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive number")]
        public double Budget { get; set; }
        public string Status { get; set; }
        public DateTime ProjectDate { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; } 
         public List<Bid> Bids { get; set; }
        public List<Skill> RequiredSkills { get; set; }
        public int? SelectedFreelancerId { get; set; }
        public Freelancer SelectedFreelancer { get; set; }
        public bool IsDeleted { get; set; }
        public Project() {
            Bids = new List<Bid>();
            RequiredSkills = new List<Skill>();
        }
    }
}
