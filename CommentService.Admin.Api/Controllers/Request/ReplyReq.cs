using CommentService.Infrastructure;
using FluentValidation;


namespace CommentService.Admin.Api.Controllers.Request;

public record class ReplyReq(string Text);

public class ReplyReqValidator: AbstractValidator<ReplyReq>
{
    public ReplyReqValidator(CommentDbContext dbCtx)
    {
        RuleFor(x => x.Text).NotNull().NotEmpty().MaximumLength(500);
    }
}

