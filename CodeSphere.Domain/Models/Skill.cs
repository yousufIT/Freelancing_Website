using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Models
{
    public class Skill : Base
    {
        public string Name { get; set; }
        public List<Profile> Profiles { get; set; } 
    }
}
