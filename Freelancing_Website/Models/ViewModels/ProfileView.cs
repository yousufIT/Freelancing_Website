namespace Freelancing_Website.Models.ViewModels
{
    public class ProfileView
    {
        public int Id { get; set; }
        public string Bio { get; set; }
        public List<SkillView> Skills { get; set; }
        public ProfileView()
        {
            Skills = new List<SkillView>();
        }
    }
}
