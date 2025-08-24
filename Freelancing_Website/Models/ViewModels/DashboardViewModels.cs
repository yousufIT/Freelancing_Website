using System.Collections.Generic;

namespace Freelancing_Website.Models.ViewModels
{
    public class DashboardSummaryView
    {
        public int TotalClients { get; set; }
        public int TotalFreelancers { get; set; }
        public int TotalProjects { get; set; }
        public int TotalBids { get; set; }
        public int TotalReviews { get; set; }
        public int TotalSkills { get; set; }

        public List<RecentProjectView> RecentProjects { get; set; }
        public List<TopFreelancerView> TopFreelancers { get; set; }
        public List<RecentBidView> RecentBids { get; set; }
    }

    public class RecentProjectView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ClientName { get; set; }
        public double Budget { get; set; }
        public string Status { get; set; }
        public int BidCount { get; set; }
    }

    public class TopFreelancerView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public int CompletedProjectsCount { get; set; }
    }

    public class RecentBidView
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Proposal { get; set; }
        public string FreelancerName { get; set; }
        public string ProjectTitle { get; set; }
    }
}
