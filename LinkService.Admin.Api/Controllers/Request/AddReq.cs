using LinkService.Infrastructure;
using FluentValidation;


namespace LinkService.Admin.Api.Controllers.Request;

public record class AddReq(string Title, string? Description, Uri? LogoUrl, Uri SiteUrl);

public class AddReqValidator : AbstractValidator<AddReq>
{
    public AddReqValidator(LinkDbContext dbCtx)
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(500);
        RuleFor(x=>x.Description).NotNull().MaximumLength(1000);
        RuleFor(x => x.SiteUrl).NotNull().NotEmpty();
    }
}

