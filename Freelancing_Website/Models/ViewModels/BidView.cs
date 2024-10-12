namespace Freelancing_Website.Models.ViewModels
{
    public class BidView
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Proposal { get; set; }
        public int ProjectId { get; set; }
    }
}
