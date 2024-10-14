namespace Freelancing_Website.Models.ForCreate
{
    public class ProjectForCreate
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Budget { get; set; }
        public string Status { get; set; }
        public int ClientId { get; set; }
    }
}
