using LinkService.Domain;
using LinkService.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace LinkService.Infrastructure
{
    public class LinkRepo : ILinkRepo
    {
        private LinkDbContext _dbContext;
        public LinkRepo(LinkDbContext dbCtx)
        {
            this._dbContext = dbCtx;
        }
        public async Task<Guid> CreateAsync(Link link)
        {
            await this._dbContext.AddAsync(link);
            return link.Id;
        }

        public async Task<Link?> FindByIdAsync(Guid id)
        {
            return await this._dbContext.Links.FindAsync(id);
        }

        public async Task<Link[]> GetAllAsync()
        {
            return await this._dbContext.Links.ToArrayAsync();
        }
    }
}
