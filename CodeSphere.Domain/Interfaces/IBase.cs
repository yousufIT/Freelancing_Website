using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Models
{
    public interface IBase
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
