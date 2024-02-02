using PageService.Domain.ValueObject;
using PageService.Domain.Entity;
using PageService.Domain.Event;


namespace PageService.Domain;

public class PageDomainSer
{
    private readonly IPageRepo _pageRepo;
    public PageDomainSer(IPageRepo repo)
    {
        this._pageRepo = repo;
    }

    public Task ChangeOptionFieldAsync(Page page, AdditionalField field)
    {
        page.SetField(field);
        return Task.CompletedTask;
    }

    public async Task<bool> JudgeExist(string pageName)
    {
        Page? page = await this._pageRepo.FindByNameAsync(pageName);
        if (page == null)
            return false;
        return true;
    }

    public async Task<Guid> CreateAsync(string title, string content, string pageName, AdditionalField field)
    {
        if (await JudgeExist(pageName))
            throw new Exception($"页面名称已经存在, name = {pageName}");
        Page page = new Page(title, content,pageName, field);
        return await this._pageRepo.CreateAsync(page);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var page = await _pageRepo.FindByIdAsync(id);
        if (page == null)
            return false;

        page.SoftDelete();
        return true;
    }

}
