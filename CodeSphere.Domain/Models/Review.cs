using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int ClientId { get; set; }
        public User Client { get; set; } 
        public int FreelancerId { get; set; }
        public User Freelancer { get; set; } 
    }
}
