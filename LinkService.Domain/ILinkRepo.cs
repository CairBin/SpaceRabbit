using LinkService.Domain.Entity;

namespace LinkService.Domain
{
    public interface ILinkRepo
    {
        public Task<Guid> CreateAsync(Link link);

        public Task<Link?> FindByIdAsync(Guid id);

        public Task<Link[]> GetAllAsync();
    }
}
