using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;

namespace Freelancing_Website.Models.Profiles
{
    public class SkillProfile : AutoMapper.Profile
    {
        public SkillProfile()
        {
            CreateMap<Skill, SkillView>();
            CreateMap<SkillForCreate, Skill>();

            CreateMap<RequiredSkill, Skill>().ReverseMap();
        }
    }
}
