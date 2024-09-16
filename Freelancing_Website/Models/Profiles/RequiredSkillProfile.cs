using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;

namespace Freelancing_Website.Models.Profiles
{

    public class RequiredSkillProfile : AutoMapper.Profile
    {
        public RequiredSkillProfile()
        {
            CreateMap<RequiredSkill, RequiredSkillView>();
            CreateMap<RequiredSkillForCreate, RequiredSkill>();
        }
    }
}
