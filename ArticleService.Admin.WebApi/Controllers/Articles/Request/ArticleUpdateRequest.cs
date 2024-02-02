using ArticleService.Infrastructure;
using FluentValidation;

namespace ArticleService.Admin.WebApi.Controllers.Articles.Request;

public record ArticleUpdateRequest(string Title, string Content, Guid AuthorId, Guid CategoryId, Uri? CoverUrl,string? Password,bool IsVisible, DateTime Created,string PublicField, string PrivateField);

public class ArticleUpdateRequestValidator : AbstractValidator<ArticleUpdateRequest>
{
    public ArticleUpdateRequestValidator(ArticleDbContext dbCtx)
    {
        RuleFor(x => x.Title).NotNull().NotEmpty();
        RuleFor(x => x.Content).NotNull().NotEmpty();
        RuleFor(x => x.AuthorId).NotNull();
        RuleFor(x => x.CategoryId).NotNull();
        RuleFor(x => x.IsVisible).NotNull();
        RuleFor(x=>x.Created).NotNull();
        RuleFor(x=>x.PublicField).NotNull();
        RuleFor(x=>x.PrivateField).NotNull();
    }
}

