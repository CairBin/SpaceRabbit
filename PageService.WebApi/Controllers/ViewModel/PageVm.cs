using PageService.Domain.Entity;

namespace PageService.WebApi.Controllers.ViewModel;

public record PageVm(Guid Id, string Title, string Content, string PageName, string PublicField, DateTime Created, DateTime Updated)
{
    public static PageVm? Create(Page? page)
    {
        if(page == null)
            return null;
        return new PageVm(page.Id, page.Title, page.Content, page.PageName, page.OptionField.PublicField, page.Created, page.Updated);
    }

    public static PageVm[] Create(Page[] items)
    {
        return items.Select(e => Create(e)).ToArray();
    }
}