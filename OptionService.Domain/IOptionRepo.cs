using OptionService.Domain.Entity;


namespace OptionService.Domain
{
    public interface IOptionRepo
    {
        public Task<Guid> CreateAsync(Option option);

        public Task<Option?> FindByIdAsync(Guid? id);

        public Task<Option?> FindByOwnerIdAsync(Guid? ownerId);

        public Task<Option?> FindByNameAsync(string name);
        
    }
}
