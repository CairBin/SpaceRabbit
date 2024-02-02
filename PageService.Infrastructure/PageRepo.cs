using PageService.Domain;
using PageService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using PageService.Domain.ValueObject;


namespace PageService.Infrastructure;

public class PageRepo : IPageRepo
{
    private readonly PageDbContext _dbCtx;

    public PageRepo(PageDbContext dbCtx)
    {
        this._dbCtx = dbCtx;
    }

    public async Task<Guid> CreateAsync(Page page)
    {
        await this._dbCtx.AddAsync(page);
        return page.Id;
    }

    public async Task<Page?> FindByIdAsync(Guid id)
    {
        var page = await this._dbCtx.Pages.FindAsync(id);
        return page;
    }

    public async Task<Page?> FindByNameAsync(string name)
    {
        var page = await this._dbCtx.Pages.SingleOrDefaultAsync(x=>x.PageName == name);
        return page;
    }

    public Task<Page[]> GetAllAsync()
    {
        return this._dbCtx.Pages.ToArrayAsync();
    }

}
