using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Proposal { get; set; }
        public int FreelancerId { get; set; }
        public User Freelancer { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
