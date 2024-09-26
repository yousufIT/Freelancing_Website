using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Models
{
    public class PortfolioItem : IBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public bool IsDeleted { get; set; }
        int IBase.Id { get; set; }
    }
}
