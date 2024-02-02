using OptionService.Infrastructure;
using FluentValidation;


namespace OptionService.Admin.Api.Controllers.Request;

public record class AddReq(string Name, string Value);

public class AddReqValidator : AbstractValidator<AddReq>
{
    public AddReqValidator(OptionDbContext dbCtx)
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
        RuleFor(x => x.Value).NotEmpty().NotNull();
    }
}

