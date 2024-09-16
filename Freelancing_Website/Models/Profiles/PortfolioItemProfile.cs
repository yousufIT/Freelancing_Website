using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;

namespace Freelancing_Website.Models.Profiles
{
    public class PortfolioItemProfile : AutoMapper.Profile
    {
        public PortfolioItemProfile()
        {
            CreateMap<PortfolioItem, PortfolioItemView>();
            CreateMap<PortfolioItemForCreate, PortfolioItem>();
        }
    }
}
