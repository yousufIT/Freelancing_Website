using CodeSphere.Domain.Models;

namespace Freelancing_Website.Models.ForCreate
{
    public class FreelancerForCreate : UserForCreate
    {
        public List<Skill> Skills { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public double Hourlysalary { get; set; }

        public FreelancerForCreate()
        {
            Skills = new List<Skill>();

        }
    }
}
