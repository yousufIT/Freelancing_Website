using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;

namespace Freelancing_Website.Models.Profiles
{
    public class BidProfile : AutoMapper.Profile
    {
        public BidProfile()
        {
            CreateMap<Bid, BidView>()
                .ForMember(dest => dest.ProjectName,opt =>opt.MapFrom(src =>src.Project.Title));
            CreateMap<BidForCreate, Bid>();
        }
    }
}
