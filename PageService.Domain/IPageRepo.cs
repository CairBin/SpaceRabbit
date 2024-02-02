using PageService.Domain.Entity;
using PageService.Domain.ValueObject;

namespace PageService.Domain;

public interface IPageRepo
{
    public Task<Guid> CreateAsync(Page page);

    public Task<Page?> FindByIdAsync(Guid id);

    public Task<Page?> FindByNameAsync(string name);

    public Task<Page[]> GetAllAsync();


}
