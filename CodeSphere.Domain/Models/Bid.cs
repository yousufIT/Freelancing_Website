using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Models
{
    public class Bid : IBase
    {
        public double Amount { get; set; }
        public string Proposal { get; set; }
        public int FreelancerId { get; set; }
        public Freelancer Freelancer { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public bool IsDeleted { get; set; }
        int IBase.Id { get; set; }
    }
}
