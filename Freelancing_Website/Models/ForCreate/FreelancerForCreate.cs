using CodeSphere.Domain.Models;

namespace Freelancing_Website.Models.ForCreate
{
    public class FreelancerForCreate : UserForCreate
    {
        public ProfileForCreate Profile { get; set; }

    }
}
