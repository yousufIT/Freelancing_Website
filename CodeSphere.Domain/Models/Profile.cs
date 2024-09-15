using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string Bio { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public ICollection<PortfolioItem> Portfolio { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
