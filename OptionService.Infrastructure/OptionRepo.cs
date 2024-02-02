using OptionService.Domain.Entity;
using OptionService.Domain;
using Microsoft.EntityFrameworkCore;

namespace OptionService.Infrastructure
{
    public class OptionRepo : IOptionRepo
    {
        private readonly OptionDbContext _dbCtx;

        public OptionRepo(OptionDbContext dbCtx)
        {
            this._dbCtx = dbCtx;
        }

        public async Task<Guid> CreateAsync(Option option)
        {
            await _dbCtx.AddAsync(option);
            return option.Id;
        }

        public async Task<Option?> FindByIdAsync(Guid? id)
        {
            return await _dbCtx.Options.FindAsync(id);
        }

        public async Task<Option?> FindByOwnerIdAsync(Guid? ownerId)
        {
            return await _dbCtx.Options.SingleOrDefaultAsync(x => x.OwnerId == ownerId);
        }

        public async Task<Option?> FindByNameAsync(string name)
        {
            return await _dbCtx.Options.SingleOrDefaultAsync(x => x.Name == name);
        }
    }
}
