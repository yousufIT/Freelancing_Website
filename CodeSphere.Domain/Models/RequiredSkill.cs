using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Models
{
    public class RequiredSkill : IBase
    {
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public bool IsDeleted { get; set; }
        int IBase.Id { get; set; }
    }
}
