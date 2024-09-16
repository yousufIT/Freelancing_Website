using AutoMapper;
using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;

namespace Freelancing_Website.Models.Profiles
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserView>();
            CreateMap<UserForCreate, User>();
        }
    }
}
