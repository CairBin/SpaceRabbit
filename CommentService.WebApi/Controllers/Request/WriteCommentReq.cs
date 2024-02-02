using CommentService.Infrastructure;
using FluentValidation;

namespace CommentService.WebApi.Controllers.Request;

public record WriteCommentReq(Guid ContentId, Guid? Parent, int Type, string Text, string Username, Uri? SiteUrl, string Email, string GoogleToken, bool IsPrivate);

public class WriteCommentReqValidator:AbstractValidator<WriteCommentReq>
{
    public WriteCommentReqValidator(CommentDbContext dbCtx)
    {
        RuleFor(x => x.ContentId).NotNull().NotEmpty();
        RuleFor(x => x.Type).NotNull();
        RuleFor(x => x.Text).NotNull().NotEmpty().MaximumLength(500);
        RuleFor(x => x.Username).NotEmpty().NotEmpty().MaximumLength(500);
        RuleFor(x=>x.Email).NotNull().NotEmpty().MaximumLength(500);
        RuleFor(x=>x.GoogleToken).NotNull().NotEmpty();
        RuleFor(x => x.IsPrivate).NotNull();
    }
}
