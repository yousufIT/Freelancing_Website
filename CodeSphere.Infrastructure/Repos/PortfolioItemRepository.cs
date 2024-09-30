using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using CodeSphere.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Infrastructure.Repos
{
    public class PortfolioItemRepository : Repository<PortfolioItem>, IPortfolioItemRepository
    {
        public PortfolioItemRepository(CodeSphereContext context, ILogger<Repository<PortfolioItem>> logger)
            : base(context, logger)
        {
        }

        public async Task<DataWithPagination<PortfolioItem>> GetPortfolioItemsByProfileIdAsync(int profileId, int pageNumber, int pageSize)
        {
            var portfolioItems = await _context.Set<PortfolioItem>()
                .Where(pi => pi.ProfileId == profileId && !pi.IsDeleted)
                .ToListAsync();
            var totalItemCount = portfolioItems.Count();

            var paginationData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            DataWithPagination<PortfolioItem> result = new DataWithPagination<PortfolioItem>();
            result.PaginationMetaData = paginationData;
            result.Items = portfolioItems;
            return result;
        }

    }
}
