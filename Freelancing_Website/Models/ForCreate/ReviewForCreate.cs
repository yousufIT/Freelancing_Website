namespace Freelancing_Website.Models.ForCreate
{
    public class ReviewForCreate
    {
        public double Rating { get; set; }
        public string Comment { get; set; }
        public int ClientId { get; set; }
        public int FreelancerId { get; set; }
    }
}
