using CommentService.Domain.Entity;
using CommentService.Domain;
using CommentService.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;


namespace CommentService.Infrastructure;

public class CommentRepo : ICommentRepo
{
    private readonly CommentDbContext _dbCtx;

    public CommentRepo(CommentDbContext dbCtx)
    {
        this._dbCtx = dbCtx;
    }

    public async Task<Guid> CreateAsync(Comment comment)
    {
        await this._dbCtx.Comments.AddAsync(comment);
        return comment.Id;
    }

    public Task<Comment[]> FindByAuthorAsync(Guid authorId)
    {
        return this._dbCtx.Comments.Where(x => x.AuthorId == authorId).ToArrayAsync();
    }

    public async Task<Comment?> FindByIdAsync(Guid id)
    {
        return await this._dbCtx.Comments.FindAsync(id);
    }

    public async Task<Comment[]> FindBySubmitterNameAsync(string name)
    {
        return await this._dbCtx.Comments.Where(x => x.Submitter.Name == name).ToArrayAsync();
    }

    public async Task<Comment[]> GetAllAsync()
    {
        return await this._dbCtx.Comments.ToArrayAsync();
    }

    public Task<Comment[]> GetArticleCommentAsync(Guid articleId)
    {
        return this._dbCtx.Comments.Where(x=>x.ContentId == articleId && x.Type == (int)Comment.CommentType.Article).ToArrayAsync();
    }

    public Task<Comment[]> GetPageCommentAsync(Guid pageId)
    {
        return this._dbCtx.Comments.Where(x => x.ContentId == pageId && x.Type == (int)Comment.CommentType.Page).ToArrayAsync();
    }

    public Task<Comment[]> GetNormalComments()
    {
        return this._dbCtx.Comments.Where(x=>x.Status == (int)Comment.CommentStatus.Normal).ToArrayAsync();
    }

    public Task<Comment[]> GetSpamComments()
    {
        return this._dbCtx.Comments.Where(x => x.Status == (int)Comment.CommentStatus.Spam).ToArrayAsync();
    }

    public Task<Comment[]> GetPrivateComments()
    {
        return this._dbCtx.Comments.Where(x => x.Status == (int)Comment.CommentStatus.Private).ToArrayAsync();
    }
}
