using ArticleService.Infrastructure;
using FluentValidation;

namespace ArticleService.Admin.WebApi.Controllers.Articles.Request;

public record ArticleAddRequest(string Title,string Content,Guid CategoryId, Uri? CoverUrl,DateTime Created, string? password, string publicField, string privateField);

public class ArticleAddRequestValidator:AbstractValidator<ArticleAddRequest>
{
    public ArticleAddRequestValidator(ArticleDbContext dbCtx)
    {
        RuleFor(x => x.Title).NotNull().NotEmpty();
        RuleFor(x => x.Content).NotNull().NotEmpty();
        RuleFor(x => x.CategoryId).NotNull();
        RuleFor(x=>x.publicField).NotNull();
        RuleFor(x=>x.privateField).NotNull();
        RuleFor(x=>x.Created).NotNull();
    }
}

