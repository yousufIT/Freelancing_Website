using CodeSphere.Domain.Models;

namespace Freelancing_Website.Models.ForCreate
{
    public class FreelancerForCreate : UserForCreate
    {
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public double Hourlysalary { get; set; }

    }
}
