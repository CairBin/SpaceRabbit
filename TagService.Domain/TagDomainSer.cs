using TagService.Domain.Entity;
using TagService.Domain.Event;
using TagService.Domain;
using MediatR;

namespace TagService.Domain;

public class TagDomainSer
{
    private readonly ITagRepo _repo;

    public TagDomainSer(ITagRepo repo)
    {
        this._repo = repo;
    }

    public async Task<Guid> CreateAsync(string tagName,Guid articleId)
    {
        Tag tag = new Tag(tagName,articleId);
        return await _repo.CreateAsync(tag);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var tag = await _repo.FindByIdAsync(id);

        if (tag == null)
            return false;

        tag.SoftDelete();
        
        return true;
    }

}
