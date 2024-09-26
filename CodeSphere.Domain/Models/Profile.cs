using CodeSphere.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Models
{
    public class Profile : IBase
    {
        public string Bio { get; set; }
        public List<Skill> Skills { get; set; }
        public List<PortfolioItem> Portfolio { get; set; }
        public int FreelancerId { get; set; }
        public Freelancer Freelancer { get; set; }
        public bool IsDeleted { get; set; }
        int IBase.Id { get; set; }
    }
}
