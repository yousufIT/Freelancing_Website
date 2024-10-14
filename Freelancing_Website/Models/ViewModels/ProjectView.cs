namespace Freelancing_Website.Models.ViewModels
{
    public class ProjectView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Budget { get; set; }
        public string Status { get; set; }
        public int ClientId { get; set; }
        public List<SkillView> RequiredSkills { get; set; }
        public ProjectView()
        {
            RequiredSkills = new List<SkillView>();
        }
    }
}
