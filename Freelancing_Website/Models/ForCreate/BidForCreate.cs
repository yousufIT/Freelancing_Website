namespace Freelancing_Website.Models.ForCreate
{
    public class BidForCreate
    {
        public double Amount { get; set; }
        public string Proposal { get; set; }
        public int ProjectId { get; set; }
        public int FreelancerId { get; set; }
    }
}
