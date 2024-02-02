using CommentService.Domain.Entity;
using CommentService.Domain.ValueObject;


namespace CommentService.Domain;

public class CommentDomainSer
{
    private readonly ICommentRepo _repo;
    public CommentDomainSer(ICommentRepo repo)
    {
        this._repo = repo;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var comment  = await this._repo.FindByIdAsync(id);
        if (comment == null)
            return false;

        comment.SoftDelete();

        return true;
    }

}
