using CommentService.Domain.Entity;
using CommentService.Domain.ValueObject;


namespace CommentService.WebApi.Controllers.ViewModel;

public record class CommentVm(
    Guid ContentId,
    int Type,
    Guid? Parent,
    string Text,
    string UserName,
    Uri? SiteUrl,
    string? Agent
)
{
    public static CommentVm? Create(Comment? comment)
    {
        if (comment == null)
            return null;
        
        if(comment.Status == (int)Comment.CommentStatus.Private)
            return new CommentVm(comment.ContentId, comment.Type, comment.Parent, "Private Comment", comment.Submitter.Name, comment.Submitter.SiteUrl, comment.Submitter.Agent);

        return new CommentVm(comment.ContentId, comment.Type, comment.Parent, comment.Text, comment.Submitter.Name, comment.Submitter.SiteUrl, comment.Submitter.Agent);
    }


    public static CommentVm[] Create(Comment[] comments)
    {
        return comments.Select(e => Create(e)).ToArray();
    }
}

