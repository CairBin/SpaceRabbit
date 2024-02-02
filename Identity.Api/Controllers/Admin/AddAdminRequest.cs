using FluentValidation;
namespace IdentityService.Api.Controllers.Admin;

public record AddAdminRequest(string Username, String Email);

public class AddAdminRequestValidator : AbstractValidator<AddAdminRequest>
{
    public AddAdminRequestValidator()
    {
        RuleFor(e => e.Email).NotNull().NotEmpty();
        RuleFor(e => e.Username).NotEmpty().NotEmpty().MaximumLength(20).MinimumLength(2);
    }
}