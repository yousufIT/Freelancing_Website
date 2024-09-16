using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;

namespace Freelancing_Website.Models.Profiles
{
    public class ReviewProfile : AutoMapper.Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewView>();
            CreateMap<ReviewForCreate, Review>();
        }
    }
}
