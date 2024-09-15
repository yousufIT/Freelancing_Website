using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Profile> Profiles { get; set; } 
    }
}
