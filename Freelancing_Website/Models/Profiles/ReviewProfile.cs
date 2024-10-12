using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;

namespace Freelancing_Website.Models.Profiles
{
    public class ReviewProfile : AutoMapper.Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewView>()
            .ForMember(dest=>dest.ClientName,opt=>opt.MapFrom(src=>src.Client.Name))
            .ForMember(dest=>dest.FreelancerName,opt=>opt.MapFrom(src=>src.Freelancer.Name));
            CreateMap<ReviewForCreate, Review>();
        }
    }
}
