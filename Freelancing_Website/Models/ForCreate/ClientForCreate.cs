using CodeSphere.Domain.Models;

namespace Freelancing_Website.Models.ForCreate
{
    public class ClientForCreate : UserForCreate
    {
        public string CompanyName { get; set; }
        public string ContactNumber { get; set; }

    }
}
