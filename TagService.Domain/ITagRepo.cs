using TagService.Domain.Entity;

namespace TagService.Domain;

public interface ITagRepo
{
    public Task<Guid> CreateAsync(Tag tag);

    public Task<Tag?> FindByIdAsync(Guid id);

    public Task<Tag[]> FindByArticleId(Guid articleId);
}
